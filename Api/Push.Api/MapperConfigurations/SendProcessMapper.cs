using AutoMapper;
using Push.Api.DTOs;
using Push.Service.ProcessService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.MapperConfigurations
{
    public class SendProcessMapper : Profile
    {
        public SendProcessMapper()
        {
            CreateMap<SendMsgDto, SendProcessDto>();
            CreateMap<SendProcessDto, PushSendProcessDomainModel>();
            CreateMap<PushSendProcessDomainModel, SendProcessDto>();
            CreateMap<SendProcessDto, ProcessHistoryDto>();

        }
    }
}
