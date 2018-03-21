using AutoMapper;
using Push.Service.ProcessService.DBModel;
using Push.Service.ProcessService.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.ProcessService.MapperConfiguration
{
    public class PushSendProcessMapper : Profile
    {
        public PushSendProcessMapper()
        {
            CreateMap<PushSendProcessDomainModel, PushSendProcess>();
            CreateMap<PushSendProcess, PushSendProcessDomainModel>();
        }
    }
}
