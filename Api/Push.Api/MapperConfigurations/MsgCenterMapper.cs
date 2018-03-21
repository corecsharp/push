using AutoMapper;
using Push.Api.DTOs;
using Push.Service.MessageCenterService.DomainModel;
using Push.Service.MessageCenterService.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Api.MapperConfigurations
{
    public class MsgListMapper : Profile
    {
        public MsgListMapper()
        {
            CreateMap<PushMessageDomainModel, MsgListResponseDto>()
                .ForMember(dto => dto.IsRead, opt => opt.MapFrom(dm => dm.State == (int)ReadState.Read));
        }
    }
}
