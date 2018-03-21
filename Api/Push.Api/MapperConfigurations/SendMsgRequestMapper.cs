using AutoMapper;
using Push.Api.DTOs;
using Push.Service.MessageCenterService.DomainModel;
using Push.Service.ProcessService.DomainModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.MapperConfigurations
{
    public class SendMsgRequestMapper : Profile
    {
        public SendMsgRequestMapper()
        {
            CreateMap<SendMsgListRequest, SendMsgRequest>();
            CreateMap<SendMsgRequest, PushMessageDomainModel>()
                .ForMember(dto => dto.UserId, opt => opt.MapFrom(dm => dm.Token))
                .ForMember(dto => dto.MessageType, opt => opt.MapFrom(dm => dm.MsgType))
                .ForMember(dto => dto.AttachInfo, opt => opt.MapFrom(dm => dm.AttachInfo == null ? null : JsonConvert.SerializeObject(dm.AttachInfo)));
        }
    }
}
