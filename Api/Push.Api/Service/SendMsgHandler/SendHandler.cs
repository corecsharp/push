using AutoMapper;
using Push.Api.DTOs;
using Push.Api.Enum;
using Push.Api.Logic;
using Push.Core.DTOs;
using Push.Service.ProcessService.Service;
using Push.Service.TokenService.DomainModel;
using Push.Service.TokenService.Service;
using System;

using System.Collections.Generic;

using System.Linq;


namespace Push.Api.Service.SendMsgHandler
{
    public class SendHandler : ISendHandler
    {
        private IMapper _mapper;
        private IPushTokenBrandService _pushTokenBrandService;
        private ISendProcessLogic _sendProcessLogic;
        private IPushSendProcessService _pushSendProcessService;
        public SendHandler(IMapper mapper
            , IPushTokenBrandService pushTokenBrandService
            , ISendProcessLogic sendProcessLogic
            ,IPushSendProcessService pushSendProcessService
            )
        {
            _mapper = mapper;
            _pushTokenBrandService = pushTokenBrandService;
            _sendProcessLogic = sendProcessLogic;
            _pushSendProcessService = pushSendProcessService;
        }

        public  bool Handle(SendProcessDto process, out string retMsg)
        {
            retMsg = string.Empty;
            //判断RzTokenBrandId是否为空
            if (process.TokenBrandId.HasValue)
            {
                DeviceChannelDomainModel deviceChannelDomainModel = _pushTokenBrandService.GetDeviceChannelListByTokenBrandIdAsync(process.TokenBrandId.Value).Result;
                DeviceChannelDto deviceChannelDto = _mapper.Map<DeviceChannelDto>(deviceChannelDomainModel);
                if (deviceChannelDomainModel != null)
                {
                    //如果存在RzTokenBrandId
                    _sendProcessLogic.SendNewProcess(process, deviceChannelDto, out retMsg);
                    return true;
                }
                //如果不存在RzTokenBrandId，执行下面操作
            }

            List<DeviceChannelDomainModel> deviceChannelList = _pushTokenBrandService.GetDeviceChannelListAsync(process.Token, process.AppId).Result;
            if (deviceChannelList == null || deviceChannelList.Count == 0)
            {
                _sendProcessLogic.RecordErrorProcess(process, ErrorTypeEnum.Logout, retMsg);
            }
            else
            {
                foreach (var item in deviceChannelList)
                {
                    DeviceChannelDto deviceChannelDto = _mapper.Map<DeviceChannelDto>(item);
                    _sendProcessLogic.SendNewProcess(process, deviceChannelDto, out retMsg);
                }
            }
            //发送成功删除数据
            var i = _pushSendProcessService.DeleteProcessByIdAsync(process.Id).Result;
            return true;
        }

        /// <summary>
        /// 批处理，正常发送
        /// 一批推送消息，AppId和ChannelId是一致的
        /// </summary>
        /// <param name="processList"></param>
        public  bool Handle(List<SendProcessDto> processList, out string retMsg)
        {
            retMsg = string.Empty;
            return _sendProcessLogic.SendProcessList(processList, out retMsg);
        }
    }
}
