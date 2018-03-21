using AutoMapper;
using Push.Service.ChannelService.DBModel;
using Push.Service.ChannelService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZNB.Push.Service.ChannelService.MapperConfiguration
{
    public class PushChannelMapper : Profile
    {
        public PushChannelMapper()
        {
            CreateMap<PushChannel, PushChannelDomainModel>();
            CreateMap<PushChannelDomainModel, PushChannel>();
        }
    }
}
