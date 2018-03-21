using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Push.Service.TokenService.DBModel;
using Sherlock.Framework;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Sherlock.Framework.Services;
using Push.Service.TokenService.DomainModel;

namespace Push.Service.TokenService.Service
{
    /// <summary>
    /// 手机设备注册明细，包括几个推送平台的token
    /// </summary>
    public class PushTokenBrandDetailService : IPushTokenBrandDetailService
    {
        private IRepository<PushTokenBrandDetail> _repository;
        private IMapper _mapper = null;
        private IIdGenerationService _idGenerationService;


        public PushTokenBrandDetailService(IMapper mapper, IRepository<PushTokenBrandDetail> repository, IIdGenerationService idGenerationService)
        {
            _mapper = mapper;
            _repository = repository;
            _idGenerationService = idGenerationService;
        }

        /// <summary>
        ///  获取设备的Token
        /// </summary>
        /// <param name="tokenBrandId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        public async Task<PushTokenBrandDetail> GetDeviceTokenAsync(long tokenBrandId, long channelId)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushTokenBrandDetail.TokenBrandId), tokenBrandId);
            filter.AddEqual(nameof(PushTokenBrandDetail.ChannelId), channelId);
            return await _repository.QueryFirstOrDefaultAsync(filter);
        }
        /// <summary>
        /// 通过TokenBrandId获取明细列表
        /// </summary>
        /// <param name="tokenBrandId"></param>
        /// <returns></returns>
        public async Task<List<PushTokenBrandDetail>> GetDeviceTokenListAsync(long tokenBrandId)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushTokenBrandDetail.TokenBrandId), tokenBrandId);
            var res = await _repository.QueryAsync(filter);
            return res.ToList();
        }
        /// <summary>
        /// 插入明细
        /// </summary>
        /// <param name="domainModel"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(PushTokenBrandDetailDomainModel domainModel)
        {
            if (domainModel == null) return 0;
            //如果设备旧的通道不存在或者通道的deviceToken发生更改，进行插入或者更新操作。
            var oldDeviceChannel = await GetDeviceTokenAsync(domainModel.TokenBrandId, domainModel.ChannelId);
            if(oldDeviceChannel == null)
            {
                var entity = _mapper.Map<PushTokenBrandDetail>(domainModel);
                entity.Id = _idGenerationService.GenerateId();
                return await _repository.InsertAsync(entity);
            }
            else
            {
                if(oldDeviceChannel.DeviceToken != domainModel.DeviceToken)
                {
                    oldDeviceChannel.DeviceToken = domainModel.DeviceToken;
                    return await _repository.UpdateAsync(oldDeviceChannel);
                }
                else
                {
                    //成功执行一条记录
                    return 1;
                }
            }
        }
        /// <summary>
        /// 通过TokenBrandId删除注册明细
        /// </summary>
        /// <param name="tokenBrandId"></param>
        /// <returns></returns>
        public async Task<int> DeleteByTokenBrandIdAsync(long tokenBrandId)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushTokenBrandDetail.TokenBrandId), tokenBrandId);
            return await _repository.DeleteAsync(filter);
        }
    }
}