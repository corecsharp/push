using AutoMapper;
using Push.Service.TokenService.DBModel;
using Push.Service.TokenService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZNB.Push.Service.TokenService.MapperConfiguration
{
    public class PushTokenBrandDetailMapper : Profile
    {
        public PushTokenBrandDetailMapper()
        {
            CreateMap<PushTokenBrandDetail, PushTokenBrandDetailDomainModel>();
            CreateMap<PushTokenBrandDetailDomainModel, PushTokenBrandDetail>();
        }
    }
}
