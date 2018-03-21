using AutoMapper;
using Push.Service.ConfigService.DBModel;
using Push.Service.ConfigService.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.ConfigService.MapperConfiguration
{
    public class InfraDicMapper : Profile
    {
        public InfraDicMapper()
        {
            CreateMap<InfraDic, InfraDicDomainModel>();
            CreateMap<InfraDicDomainModel, InfraDic>();
        }
    }
}
