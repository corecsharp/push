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
    public class DeviceChannelMapper : Profile
    {
        public DeviceChannelMapper()
        {
            CreateMap<DeviceChannelDomainModel, DeviceChannelDto>();
        }
    }
}
