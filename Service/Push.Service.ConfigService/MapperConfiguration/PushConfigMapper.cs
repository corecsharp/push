using AutoMapper;
using Push.Service.ConfigService.DBModel;
using Push.Service.ConfigService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Service.ConfigService.MapperConfiguration
{
    public class PushConfigMapper : Profile
    {
        public PushConfigMapper()
        {
            CreateMap<PushConfig, PushConfigDomainModel>();
            CreateMap<PushConfigDomainModel, PushConfig>();
        }
    }
}
