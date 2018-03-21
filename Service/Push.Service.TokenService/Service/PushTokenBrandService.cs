using AutoMapper;
using Platform.Core.Extentions;
using Platform.Core.Helper;
using Platform.Core.Options;
using Push.Service.TokenService.DBModel;
using Push.Service.TokenService.DomainModel;
using Push.Service.TokenService.Repository;
using Microsoft.Extensions.Options;
using Sherlock.Framework.Caching;
using Sherlock.Framework.Data;
using Sherlock.Framework.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.TokenService.Service
{
    /// <summary>
    /// 手机设备注册
    /// </summary>
    public class PushTokenBrandService : IPushTokenBrandService
    {
        private IRepository<PushTokenBrand> _repository;
        private IPushTokenBrandRepository _pushTokenBrandRepository;
        private IMapper _mapper = null;
        private IIdGenerationService _idGeneratorService;
        private IPushTokenBrandDetailService _pushTokenBrandDetailService;
        private ICacheManager _cacheManager;
        private IOptionsSnapshot<RedisCacheKeyOptions> _redisCacheKeyOptions;


        public PushTokenBrandService(IMapper mapper
            , IRepository<PushTokenBrand> repository
            , IPushTokenBrandRepository pushTokenBrandRepository
            , IIdGenerationService idGeneratorService
             , IPushTokenBrandDetailService pushTokenBrandDetailService
            , ICacheManager cacheManager
            , IOptionsSnapshot<RedisCacheKeyOptions> redisCacheKeyOptions
            )
        {
            _mapper = mapper;
            _repository = repository;
            _pushTokenBrandRepository = pushTokenBrandRepository;
            _idGeneratorService = idGeneratorService;
            _pushTokenBrandDetailService = pushTokenBrandDetailService;
            _cacheManager = cacheManager;
            _redisCacheKeyOptions = redisCacheKeyOptions;
        }

        /// <summary>
        /// 获取通过Token获取注册列表
        /// </summary>
        /// <param name="RzToken"></param>
        /// <returns></returns>
        public async Task<List<PushTokenBrandDomainModel>> GetTokenBrandListByTokenAsync(string token)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushTokenBrand.Token), token);
            var res = await _repository.QueryAsync(filter);
            List<PushTokenBrand> list = res.ToList();
            return _mapper.Map<List<PushTokenBrandDomainModel>>(list);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="domainModel"></param>
        /// <returns></returns>
        public async Task<int> InsertTokenBrandAsync(RegisterRequestDomainModel domainModel)
        {
            if (domainModel == null)
            {
                return 0;
            }
            var oldTokenBrand = await GetTokenBrandAsync(domainModel.Token, domainModel.AppId?.ToString() ?? "", domainModel.DeviceId);

            PushTokenBrand entity = null;
            if (oldTokenBrand == null)
            {
                entity = _mapper.Map<RegisterRequestDomainModel, PushTokenBrand>(domainModel);
                entity.CreateAt = entity.UpdateAt = DateTimeHelper.GetNow();
                entity.Id = _idGeneratorService.GenerateId();
                int row = await _repository.InsertAsync(entity);
            }
            else
            {
                entity = oldTokenBrand;
            }

            foreach (var detail in domainModel.DeviceTokens)
            {
                detail.TokenBrandId = entity.Id;
                detail.CreateAt = detail.UpdateAt = DateTimeHelper.GetNow();
                await _pushTokenBrandDetailService.InsertAsync(detail);
            }
            return domainModel.DeviceTokens.Count() + 1;
        }

        private async Task<PushTokenBrand> GetTokenBrandAsync(string token, string appId, string deviceId)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushTokenBrand.Token), token);
            filter.AddEqual(nameof(PushTokenBrand.AppId), appId);
            filter.AddEqual(nameof(PushTokenBrand.DeviceId), deviceId);
            return await _repository.QueryFirstOrDefaultAsync(filter);
        }

        /// <summary>
        /// 根据id删除TokenBrand
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteTokenBrandByIdAsync(long id)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushTokenBrand.Id), id);
            int deleteRow = await _repository.DeleteAsync(filter);
            //删除注册详情表
            int deleteDetailRows = await _pushTokenBrandDetailService.DeleteByTokenBrandIdAsync(id);
            return deleteRow + deleteDetailRows;
        }

        /// <summary>
        /// 通过Id获取TokenBrand
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PushTokenBrandDomainModel> GetTokenBrandByIdAsync(long id)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushTokenBrand.Id), id);
            PushTokenBrand entity = await _repository.QueryFirstOrDefaultAsync(filter);
            return _mapper.Map<PushTokenBrandDomainModel>(entity);
        }

        /// <summary>
        /// 通过消息的Token和AppId获取设备通道信息（ChannelId, URL, SystemType, AppKey, AppSecret）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<List<DeviceChannelDomainModel>> GetDeviceChannelListAsync(string token, long appId)
        {
            var res = await _pushTokenBrandRepository.GetDeviceChannelListAsync(token, appId);
            return res.ToList();
        }

        /// <summary>
        /// 通过消息的Token和AppId单个获取设备通道信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DeviceChannelDomainModel> GetDeviceChannelListByTokenBrandIdAsync(long id)
        {
            var res = await _pushTokenBrandRepository.GetDeviceChannelListByTokenBrandIdAsync(id);
            return res;
        }

        /// <summary>
        /// 尝试清空在该设备上注册的以前的用户
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<int> TryClearDeviceOldUserAsync(string deviceId, string userId)
        {
            return await _pushTokenBrandRepository.TryClearDeviceOldUser(deviceId, userId);
        }

        public async Task<List<string>> GetAllTokenBrand(int? appId)
        {
            if (appId == null)
                return null;
            var obj = new object();
            var predicate = new Predicate<object>((n) => { return true; });
            var tokenList = await _cacheManager.WithCacheQuery($"AllTokenList:{appId}",
                async () =>
                {
                    var queryResult = await _pushTokenBrandRepository.QueryAsync(p => p.AppId == appId);
                    if (queryResult == null || queryResult.Count() == 0)
                        return null;
                    return queryResult.Distinct(o => o.Token).Select(o => o.Token).ToList();
                },
                r => r != null && r.Any()
                , TimeSpan.FromMinutes(5)
                );
            return tokenList;
        }
    }
}
