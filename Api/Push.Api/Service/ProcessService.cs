
using AutoMapper;
using Platform.Core.Helper;
using Push.Api.DTOs;
using Push.Api.Logic;
using Push.Core.DTOs;
using Push.Service.ProcessService.DomainModel;
using Push.Service.ProcessService.Service;
using System;
using System.Collections.Generic;


namespace Push.Api.Service
{
    public class ProcessService: IProcessService
    {
        private IPushSendProcessService _pushSendProcessService;
        private IChannelLogic _channelLogic;
        private IMapper _mapper;
        public ProcessService(IPushSendProcessService pushSendProcessService, IChannelLogic channelLogic, IMapper mapper) {

            _pushSendProcessService = pushSendProcessService;
            _channelLogic = channelLogic;
            _mapper = mapper;

        }


        /// <summary>
        /// 从Process表取出待发送的IdList
        /// </summary>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        public List<long> GetProcessIdList(out string retMsg)
        {
            retMsg = "";
            try
            {
                var guid = Guid.NewGuid();
                var expireTime = DateTimeHelper.GetNow().AddMinutes(3);
                var updateRet = _pushSendProcessService.UpdateBatchNOAsync(guid, expireTime).Result;
                if (updateRet == 0)
                    return new List<long>();
                return _pushSendProcessService.SelectProcessIdListByBatchNOAsync(guid).Result;
            }
            catch (Exception ex)
            {
                retMsg = ex.StackTrace + "\r\n" + ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 从Process表取出待发送的BatchNO
        /// </summary>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        public string GetProcessBatchNO(out string retMsg)
        {
            retMsg = "";
            try
            {
                var timeNow = DateTimeHelper.GetNow();
                PushSendProcessAppChannelDomainModel pushSendProcessAppChannelDomainModel = _pushSendProcessService.GetAppIdAndChannelIdAsync(timeNow).Result;
                //AppChannelDto appChannelDto = _mapper.Map<AppChannelDto>(pushSendProcessAppChannelDomainModel);
                //获取第一条推送消息的AppId和ChannelId,如果没有，返回批次号为空
                if (pushSendProcessAppChannelDomainModel == null) return string.Empty;
                Guid batchNO = Guid.NewGuid();
                var expireTime = timeNow.AddMinutes(3);
                //获取通道
                ChannelDto channelDto;
                if (!_channelLogic.CheckChannel(pushSendProcessAppChannelDomainModel.ChannelId, out channelDto, out retMsg))
                {
                    return string.Empty;
                }
                int topNum = channelDto.PushNum.HasValue ? channelDto.PushNum.Value : 200;//可以从配置中获取数据,默认给200
                BatchProcessParmsDomainModel batchProcessParmsDomainModel = new BatchProcessParmsDomainModel
                {
                    TopNum = topNum,
                    AppId = pushSendProcessAppChannelDomainModel.AppId,
                    ChannelId = pushSendProcessAppChannelDomainModel.ChannelId,
                    BatchNo = batchNO,
                    TimeNow = timeNow,
                    ExpireTime = expireTime
                };
                var updateNum = _pushSendProcessService.UpdateBatchNOByAppIdAndChannelIdAsync(batchProcessParmsDomainModel).Result;
                if (updateNum == 0) return string.Empty;
                return batchNO.ToString();
            }
            catch (Exception ex)
            {
                retMsg = ex.StackTrace + "\r\n" + ex.Message;
                return null;
            }
        }
    }
}
