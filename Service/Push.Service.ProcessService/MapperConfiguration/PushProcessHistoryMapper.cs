using AutoMapper;
using Push.Service.ProcessService.DBModel;
using Push.Service.ProcessService.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.ProcessService.MapperConfiguration
{
   public class PushProcessHistoryMapper:Profile
    {
        public PushProcessHistoryMapper()
        {
            CreateMap<AddProcessHistoryDomainModel, PushProcessHistory>();
        }
    }
}
