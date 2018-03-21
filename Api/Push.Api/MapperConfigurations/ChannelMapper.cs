using AutoMapper;
using Push.Api.DTOs;
using Push.Service.ChannelService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.MapperConfigurations
{
    public class ChannelMapper : Profile
    {
        public ChannelMapper()
        {
            CreateMap<PushChannelDomainModel, ChannelDto>();
        }

    }
}
