using Push.Core.DTOs;
using Push.Core.Infrastructure;
using System.Collections.Generic;
using Push.Service.TokenService.Service;
using Push.Service.ConfigService.Service;
using Push.Api.DTOs;
using AutoMapper;
using Push.Service.ConfigService.DomainModel;
using Push.Service.TokenService.DomainModel;

namespace Push.Api.Logic
{
    public class TokenBrandLogic : ITokenBrandLogic
    {
        private IPushTokenBrandService _pushTokenBrandService;
        private IInfraDicService _infraDicService;
        private IMapper _mapper;

        public TokenBrandLogic(IPushTokenBrandService pushTokenBrandService, IInfraDicService infraDicService, IMapper mapper)
        {
            _pushTokenBrandService = pushTokenBrandService;
            _infraDicService = infraDicService;
            _mapper = mapper;
        }
        /// <summary>
        /// 检查Token是否注册
        /// </summary>
        /// <param name="RZToken"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool CheckRZToken(string RzToken, out List<TokenBrandDto> list)
        {
            list = GetRzTokenBrandListByRZToken(RzToken);
            if (list == null || list.Count == 0)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// 检查RZToken是否注册
        /// </summary>
        /// <param name="RZToken"></param>
        /// <returns></returns>
        public List<TokenBrandDto> GetRzTokenBrandListByRZToken(string RzToken)
        {
            var list = CourseCacheListLogic<string, TokenBrandDto>.Get(string.Format("RzTokenBrandList_RZToken_{0}", RzToken), () => {
                var model= _pushTokenBrandService.GetTokenBrandListByTokenAsync(RzToken).Result;
                return _mapper.Map<List<TokenBrandDto>>(model);
            });
            return list;
        }

        /// <summary>
        /// 匹配品牌编号
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public bool MatchBrandId(string brand, out int brandId)
        {
            string defaultBrand = "default";
            brandId = 0;
            brand = brand.ToLower();
            InfraDicDto infraDicDto = CourseCacheLogic<string, InfraDicDto>.Get("Brand_" + brand,
                () =>
                {
                    InfraDicDomainModel model = _infraDicService.GetDicByTypeAndValueAsync("BrandId", brand).Result;
                    return _mapper.Map<InfraDicDto>(model);
                });
            if (infraDicDto == null)
            {
                if (brand != defaultBrand)
                {
                    return MatchBrandId(defaultBrand, out brandId);
                }
                return false;
            }
            brandId = infraDicDto.Key;
            return true;
        }


        /// <summary>
        /// 检查AppId是否存在
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public bool CheckAppId(int appId, out string retMsg)
        {
            retMsg = "";
            InfraDicDto infraDicDto = CourseCacheLogic<string, InfraDicDto>.Get("AppId_" + appId,
                () =>
                {
                    InfraDicDomainModel model= _infraDicService.GetDicByTypeAndKeyAsync("AppId", appId.ToString()).Result;
                    return _mapper.Map<InfraDicDto>(model);
                });
            if (infraDicDto == null)
            {
                retMsg = string.Format("AppId:{0}不存在", appId);
                //LogHelper.Error.Write("CheckAppId", retMsg);
                return false;
            }
            return true;
        }

        public int MatchSystemType(int brandId)
        {
            if (brandId == 1)
                return (int)SystemTypeEnum.IOS;
            return (int)SystemTypeEnum.Android;
        }



        /// <summary>
        /// 通过Id,检查TokenBrand
        /// </summary>
        /// <param name="rzTokenBrandDto"></param>
        /// <param name="channelDto"></param>
        /// <param name="appChannelDto"></param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        public bool CheckRzTokenBrand(long id, out TokenBrandDto rzTokenBrandDto)
        {
            rzTokenBrandDto = CourseCacheLogic<long, TokenBrandDto>.Get(id,
               () =>
               {
                   PushTokenBrandDomainModel model= _pushTokenBrandService.GetTokenBrandByIdAsync(id).Result;
                   return _mapper.Map<TokenBrandDto>(model);
               });
            if (rzTokenBrandDto == null)
            {
                return false;
            }
            return true;
        }
    }
}
