using AutoMapper;
using Push.Service.MessageCenterService.DBModel;
using Push.Service.MessageCenterService.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.MessageCenterService.MapperConfiguration
{
    public class PushMessageMapper : Profile
    {
        public PushMessageMapper()
        {
            CreateMap<PushMessageDomainModel, PushMessage>().ReverseMap();
       
        }
    }
}
