using AutoMapper;
using Push.Service.TokenService.DBModel;
using Push.Service.TokenService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZNB.Push.Service.TokenService.MapperConfiguration
{
    public class PushTokenBrandMapper : Profile
    {
        public PushTokenBrandMapper()
        {
            CreateMap<PushTokenBrand, PushTokenBrandDomainModel>();
            CreateMap<PushTokenBrandDomainModel, PushTokenBrand>();
            CreateMap<RegisterRequestDomainModel, PushTokenBrand>();
        }
    }
}
