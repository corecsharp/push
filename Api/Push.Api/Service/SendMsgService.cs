using AutoMapper;
using Platform.Core.Helper;
using Push.Api;
using Push.Api.DTOs;
using Push.Api.Enum;
using Push.Api.Service.SendMsgHandler;
using Push.Service.ProcessService.DomainModel;
using Push.Service.ProcessService.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Push.Api.Service
{
    /// <summary>
    /// 推送消息Service
    /// </summary>
    public class SendMsgService: ISendMsgService
    {
        private IPushSendProcessService _pushSendProcessService;
        private ITimeOutHandler _timeOutHandler;
        private IRetryTimesOverHandler _retryTimesOverHandler;
        private ISendHandler _sendHandler;
        private IMapper _mapper;
        public SendMsgService(IPushSendProcessService pushSendProcessService
            , ITimeOutHandler timeOutHandler
            , IRetryTimesOverHandler retryTimesOverHandler
            , ISendHandler sendHandler
            , IMapper mapper)
        {
            _pushSendProcessService = pushSendProcessService;
            _timeOutHandler = timeOutHandler;
            _retryTimesOverHandler = retryTimesOverHandler;
            _sendHandler = sendHandler;
            _mapper = mapper;
        }

        /// <summary>
        /// 轮询推送消息
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public ErrCode SendMsg(long processId, out string ret)
        {
            ret = "";
            int rows = _pushSendProcessService.UpdateProcessUseStatusByIdAsync(processId, DateTimeHelper.GetNow().AddMinutes(2)).Result;
            if (rows == 0)
            {
                ret = "已被其他机器处理";
                return ErrCode.PermissionDenied;
            }

            PushSendProcessDomainModel pushSendProcessDomainModel = _pushSendProcessService.GetProcessByIdAsync(processId).Result;
            SendProcessDto process = _mapper.Map<SendProcessDto>(pushSendProcessDomainModel);
            var classify = ClassifyProcess(process);
            try {
                bool flag = false;
                switch (classify)
                {
                    case 1:
                        flag= _timeOutHandler.Handle(process, out ret);
                        break;
                    case 2:
                        flag = _retryTimesOverHandler.Handle(process, out ret);
                        break;
                    case 3:
                        flag = _sendHandler.Handle(process, out ret);
                        break;
                    default:
                        ret = "该分类处理没有实现";
                        break;
                }
                if (!flag)
                    return ErrCode.Failure;
            }
            catch (Exception ex)
            {
                //LogHelper.Error.Write("SendProcess", ex);
                ret = ex.StackTrace + "\r\n" + ex.Message;
                return ErrCode.Failure;
            }

            return ErrCode.Sucess;
        }
        /// <summary>
        /// 对推送信息分类
        /// </summary>
        /// <param name="process"></param>
        /// <returns>1：超时，2：重试超过3次，3：正常发送</returns>
        private int ClassifyProcess(SendProcessDto process)
        {
            var now = DateTimeHelper.GetNow();
            if (process.EndTime < now)
                return 1;
            if (process.DelayTimes >= 3)
                return 2;
            return 3;
        }



        /// <summary>
        /// 轮询推送消息
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public ErrCode SendMsgListByBatchNO(string batchNO, out string ret)
        {
            ret = "";

            List<PushSendProcessDomainModel> domainList= _pushSendProcessService.GetProcessListByBatchNOAsync(batchNO).Result;
            List<SendProcessDto> processList = _mapper.Map<List<SendProcessDto>>(domainList);
            List <SendProcessDto> timeOutList = processList.Where(t => t.EndTime < DateTimeHelper.GetNow()).ToList();
            if (timeOutList != null && timeOutList.Count > 0) {
                //超时失败
                ret = string.Empty;
                if (HandleList(timeOutList, ClassifyEnum.Timeout, out ret))
                {
                    //LogHelper.Info.Write("SendMsgListByBatchNO", "超时处理：" + ret);
                }
                else {
                    //LogHelper.Error.Write("SendMsgListByBatchNO", "超时处理：" + ret);
                }
            }
            List<SendProcessDto> retryTimesOverList = processList.Except(timeOutList).Where(t => t.DelayTimes >= 3).ToList();
            if (retryTimesOverList != null && retryTimesOverList.Count > 0) {
                //多次重试
                ret = string.Empty;
                if (HandleList(retryTimesOverList, ClassifyEnum.RetryTimesOver, out ret))
                {
                    //LogHelper.Info.Write("SendMsgListByBatchNO", "多次重试：" + ret);
                }
                else {
                    //LogHelper.Error.Write("SendMsgListByBatchNO", "多次重试：" + ret);
                }
            }
            List<SendProcessDto> sendList = processList.Except(timeOutList).Except(retryTimesOverList).ToList();
            if (sendList != null && sendList.Count > 0)
            {
                //正常发送
                ret = string.Empty;
                if (HandleList(sendList, ClassifyEnum.Send, out ret))
                {
                    //LogHelper.Info.Write("SendMsgListByBatchNO", "正常发送：" + ret);
                }
                else {
                    //LogHelper.Error.Write("SendMsgListByBatchNO", "正常发送：" + ret);
                }
            }
            return ErrCode.Sucess;
        }

        /// <summary>
        /// 分类，分情况批处理
        /// </summary>
        /// <param name="processList"></param>
        /// <param name="classify"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool HandleList(List<SendProcessDto> processList, ClassifyEnum classify,out string ret) {
            try
            {
                bool flag = false;
                switch (classify)
                {
                    case ClassifyEnum.Timeout:
                        flag = _timeOutHandler.Handle(processList, out ret);
                        break;
                    case ClassifyEnum.RetryTimesOver:
                        flag = _retryTimesOverHandler.Handle(processList, out ret);
                        break;
                    case ClassifyEnum.Send:
                        flag = _sendHandler.Handle(processList, out ret);
                        break;
                    default:
                        ret = "该分类处理没有实现";
                        break;
                }
                return flag;
            }
            catch (Exception ex)
            {
                ret = ex.StackTrace + "\r\n" + ex.Message;
                return false;
            }
        }
    }
}
