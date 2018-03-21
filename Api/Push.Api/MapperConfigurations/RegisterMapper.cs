using AutoMapper;
using Push.Api.DTOs;
using Push.Service.TokenService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.MapperConfigurations
{
    public class RegisterMapper : Profile
    {
        /// <summary>
        /// 注册相关
        /// </summary>
        public RegisterMapper()
        {
            CreateMap<RegisterRequestDto, RegisterRequestDomainModel>();
            CreateMap<TokenBrandDetailDto, PushTokenBrandDetailDomainModel>();
        }
    }
}
