using AutoMapper;
using Push.Api.DTOs;
using Push.Service.ConfigService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.MapperConfigurations
{
   public class InfraDicMapper: Profile
    {
        public InfraDicMapper()
        {
            CreateMap<InfraDicDomainModel, InfraDicDto>();
        }
    }
}
