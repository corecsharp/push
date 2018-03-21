using AutoMapper;
using Push.Api.DTOs;
using Push.Api.Logic;
using Push.Service.TokenService.DomainModel;
using Push.Service.TokenService.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Push.Api.Service
{
    public class RegisterService : IRegisterService
    {
        private ITokenBrandLogic _tokenBrandLogic;
        private IPushTokenBrandService _pushTokenBrandService;
        private IMapper _mapper;
        private const int APPLE_BRAND_ID= 1;
        public RegisterService(ITokenBrandLogic tokenBrandLogic, IPushTokenBrandService pushTokenBrandService,IMapper mapper)
        {
            _tokenBrandLogic = tokenBrandLogic;
            _pushTokenBrandService = pushTokenBrandService;
            _mapper = mapper;
        }


        public ErrCode Register(RegisterRequestDto dto, out string retMsg)
        {
            retMsg = "";
            try
            {
                int brandId;
                if (!_tokenBrandLogic.MatchBrandId(dto.Brand, out brandId))
                {
                    return ErrCode.DataIsnotExist;
                }
                int systemType = brandId == APPLE_BRAND_ID ? 0 : 1;
                //获取通过RToken获取注册列表
                var tokenBrandList = _pushTokenBrandService.GetTokenBrandListByTokenAsync(dto.Token).Result;
                var tokenBrands = tokenBrandList.Where(e => e.SystemType == systemType).OrderByDescending(e => e.UpdateAt).ToList();
                var tokenBrand = tokenBrands.SingleOrDefault(e => e.AppId == dto.AppId && e.BrandId == brandId && e.DeviceId == dto.DeviceId);

                if (tokenBrands.Count() >= 3)
                {
                    for (int i = 2; i < tokenBrands.Count(); i++)
                    {
                        int deleteRow = _pushTokenBrandService.DeleteTokenBrandByIdAsync(tokenBrands[i].Id).Result;
                    }
                }

                //检查AppId是否存在
                if (!_tokenBrandLogic.CheckAppId(dto.AppId.Value, out retMsg))
                {
                    return ErrCode.AppNotExist;
                }

                //清除设备之前注册的用户信息，避免设备收到别的用户的消息。
                _pushTokenBrandService.TryClearDeviceOldUserAsync(dto.DeviceId, dto.Token).Wait();

                RegisterRequestDomainModel model = _mapper.Map<RegisterRequestDomainModel>(dto);
                model.SystemType = systemType;
                model.BrandId = brandId;
                var ret = _pushTokenBrandService.InsertTokenBrandAsync(model).Result;
                if (ret > 0)
                    return ErrCode.Sucess;
                return ErrCode.Failure;
            }
            catch (Exception ex)
            {
                retMsg = ex.Message;
                return ErrCode.Failure;
            }
        }

        public ErrCode Unregister(UnregisterRequestDto dto, out string retMsg)
        {
            retMsg = "";
            try
            {
               List<PushTokenBrandDomainModel> tokenBrandList = _pushTokenBrandService.GetTokenBrandListByTokenAsync(dto.Token).Result;
                var tokenBrands = tokenBrandList.Where(e => e.AppId == dto.AppId && e.DeviceId == dto.DeviceId).ToList();
                if (tokenBrands.Count() > 0)
                {
                    for (int i = 0; i < tokenBrands.Count(); i++)
                    {
                       int deleteRows= _pushTokenBrandService.DeleteTokenBrandByIdAsync(tokenBrands[i].Id).Result;
                    }
                }
                return ErrCode.Sucess;
            }
            catch (Exception ex)
            {
                retMsg = ex.Message;
                return ErrCode.Failure;
            }
        }

       
    }
}
