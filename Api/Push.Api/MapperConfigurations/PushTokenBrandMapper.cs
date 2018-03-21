using AutoMapper;
using Push.Api.DTOs;
using Push.Service.TokenService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Api.MapperConfiguration
{
    public class PushTokenBrandMapper : Profile
    {
        public PushTokenBrandMapper()
        {
            CreateMap<PushTokenBrandDomainModel, TokenBrandDto>();
        }
    }
}
