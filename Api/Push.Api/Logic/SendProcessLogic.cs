using Push.Core.DTOs;
using Push.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using AutoMapper;
using Platform.Core.Helper;
using Push.Service.ProcessService.Service;
using Push.Api.DTOs;
using Push.Api.Enum;
using Push.Api.Logic.Common;
using Push.Service.ProcessService.DomainModel;

namespace Push.Api.Logic
{
    /// <summary>
    /// 推送消息的逻辑
    /// </summary>
    public class SendProcessLogic : ISendProcessLogic
    {
        private IPushProcessHistoryService _pushProcessHistoryService;
        private IMapper _mapper;
        private IConfigLogic _configLogic;
        private IPushSendProcessService _pushSendProcessService;
        private IChannelLogic _channelLogic;
        private ITokenBrandLogic _tokenBrandLogic;
        public SendProcessLogic(IPushProcessHistoryService pushProcessHistoryService
            , IMapper mapper
            , IConfigLogic configLogic
            , IPushSendProcessService pushSendProcessService
            , IChannelLogic channelLogic
            , ITokenBrandLogic tokenBrandLogic
            )
        {
            _pushProcessHistoryService = pushProcessHistoryService;
            _mapper = mapper;
            _configLogic = configLogic;
            _pushSendProcessService = pushSendProcessService;
            _channelLogic = channelLogic;
            _tokenBrandLogic = tokenBrandLogic;
        }

        /// <summary>
        /// 备案错误推送消息
        /// </summary>
        /// <param name="sendProcessDto">推送消息</param>
        /// <param name="errorStatus">错误类型</param>
        /// <param name="errorRemark">错误信息</param>
        /// <returns></returns>
        public void RecordErrorProcess(SendProcessDto sendProcessDto, ErrorTypeEnum errorType, string errorRemark)
        {
            if (sendProcessDto == null)
            {
                //LogHelper.Error.Write("RecordErrorProcess", "参数错误");
                return;
            }
            ProcessHistoryDto processHistoryDto = _mapper.Map<ProcessHistoryDto>(sendProcessDto);
            processHistoryDto.Id = 0;
            processHistoryDto.SendStatus = (int)SendStatusEnum.Failure;
            processHistoryDto.ErrorType = (int)errorType;
            processHistoryDto.Remark = errorRemark;
            AddProcessHistoryDomainModel model = _mapper.Map<AddProcessHistoryDomainModel>(processHistoryDto);
            int row = _pushProcessHistoryService.InsertProcessHistoryAsync(model).Result;
        }

        /// <summary>
        /// 推送消息给推送平台
        /// </summary>
        /// <param name="sendProcessDto">推送消息</param>
        /// <param name="rzTokenBrandDto">注册信息</param>
        /// <param name="rzTokenBrandDetailDto">注册明细</param>
        /// <param name="channelDto">通道</param>
        /// <param name="appChannelDto">App和通道的关系</param>
        /// <param name="requestTime">请求时间</param>
        /// <returns></returns>
        public SenderRet SendPushMsgToProvider(SendProcessDto sendProcessDto, DeviceChannelDto deviceChannelDto, out int requestTime)
        {
            SenderRet senderRet = new SenderRet { IsSuccess = true };
            //底层发送消息
            requestTime = 0;
            //获取ProductionMode
            object ProducttMode = _configLogic.GetConfigValue(ConfigKey.ProductionMode);
            //构造底层推送消息的参数
            PushChannelModel pushChannelModel = new PushChannelModel
            {
                ChannelName = deviceChannelDto.ChannelName,
                Url = deviceChannelDto.Url,
                AppKey = deviceChannelDto.AppKey,
                AppSecret = deviceChannelDto.AppSecret,
                ProductionMode = Convert.ToBoolean(ProducttMode)      //获取调试模式
            };
            PushMsgModel pushMsgModel = new PushMsgModel
            {
                Ticker = sendProcessDto.Title,
                Title = sendProcessDto.Title,
                Msg = sendProcessDto.Msg,
                AttachInfo = sendProcessDto.AttachInfo,
                DeviceToken = deviceChannelDto.DeviceToken,
                SystemType = (SystemTypeEnum)deviceChannelDto.SystemType
            };
            ISender sender = PushSenderManager.GetSender(deviceChannelDto.ChannelId);
            if (sender == null)
            {

                senderRet.IsSuccess = false;
                senderRet.Msg = string.Format("ChannelId:{0},推送信息供应商不存在", deviceChannelDto.ChannelId);
                //LogHelper.Error.Write("SendPushMsgToProvider", senderRet.Msg);
                return senderRet;
            }
            Stopwatch sw = new Stopwatch();
            var isRealPushMsg = _configLogic.GetConfigValue(ConfigKey.IsRealPushMsg);
            sw.Start();
            if (isRealPushMsg != null && Convert.ToBoolean(isRealPushMsg))
            {
                senderRet = PushSender.Send(sender, pushChannelModel, pushMsgModel);
            }
            else
            {
                Thread.Sleep(200);
                senderRet = new SenderRet { IsSuccess = true, Sign = Guid.NewGuid().ToString() };
            }
            sw.Stop();
            requestTime = (int)sw.ElapsedMilliseconds;
            return senderRet;
        }

        /// <summary>
        /// 推送消息之后执行DB操作
        /// </summary>
        /// <param name="senderRet">发送记录</param>
        /// <param name="sendProcessDto">推送消息</param>
        /// <param name="rzTokenBrandDto">注册信息</param>
        /// <param name="rzTokenBrandDetailDto">注册明细</param>
        /// <param name="channelDto">通道</param>
        /// <param name="requestTime">执行时间</param>
        public void SendMsgToDB(SenderRet senderRet, SendProcessDto sendProcessDto, DeviceChannelDto deviceChannelDto, int requestTime)
        {
            if (!senderRet.IsSuccess)
            {
                //发送失败的逻辑
                DateTime sendTime = DateTime.Now.AddSeconds((sendProcessDto.EndTime - DateTimeHelper.GetNow()).TotalSeconds / 2);
                if (sendProcessDto.TokenBrandId.HasValue)
                {
                    int row = _pushSendProcessService.WriteBackProcessByIdAsync(sendProcessDto.Id, sendTime).Result;
                }
                else
                {
                    //重新制造RzTokenBrandId有值的推送信息
                    SendProcessDto newSendProcess = sendProcessDto;
                    newSendProcess.Id = 0;
                    newSendProcess.TokenBrandId = deviceChannelDto.Id;
                    newSendProcess.SendTime = sendTime;
                    newSendProcess.IsUsed = false;
                    newSendProcess.DelayTimes = newSendProcess.DelayTimes + 1;
                    PushSendProcessDomainModel pushSendProcessDomainModel = _mapper.Map<PushSendProcessDomainModel>(newSendProcess);
                    int row = _pushSendProcessService.InsertProcessAsync(pushSendProcessDomainModel).Result;
                }
            }
            else if (sendProcessDto.TokenBrandId.HasValue)
            {
                //发送成功并且RzTokenBrandId不为空,直接删掉待发送的数据
                int row = _pushSendProcessService.DeleteProcessByIdAsync(sendProcessDto.Id).Result;
            }
            //无论发送成功与否，都要插入历史记录
            ProcessHistoryDto processHistoryDto = _mapper.Map<ProcessHistoryDto>(sendProcessDto);
            processHistoryDto.Id = 0;
            processHistoryDto.SendTime = DateTimeHelper.GetNow();
            if (senderRet.IsSuccess)
            {
                processHistoryDto.SendStatus = (int)SendStatusEnum.Success;
            }
            else
            {
                processHistoryDto.SendStatus = (int)SendStatusEnum.Failure;
                processHistoryDto.ErrorType = (int)ErrorTypeEnum.PushPlatform;
            }
            processHistoryDto.ChannelId = deviceChannelDto.ChannelId;
            processHistoryDto.BrandId = deviceChannelDto.BrandId;
            processHistoryDto.RequestTime = requestTime;
            processHistoryDto.ReturnSign = senderRet.Sign;
            processHistoryDto.Remark = senderRet.Code;
            AddProcessHistoryDomainModel model = _mapper.Map<AddProcessHistoryDomainModel>(processHistoryDto);
            int res = _pushProcessHistoryService.InsertProcessHistoryAsync(model).Result;
        }

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="sendProcessDto">推送消息</param>
        /// <param name="rzTokenBrandDto">注册信息</param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        public bool SendNewProcess(SendProcessDto sendProcessDto, DeviceChannelDto deviceChannelDto, out string retMsg)
        {
            Stopwatch stop = new Stopwatch();
            retMsg = string.Empty;
            if (sendProcessDto == null || deviceChannelDto == null)
            {
                retMsg = "SendProcessDto或DeviceChannelDto为空";
                return false;
            }
            stop.Start();
            //底层发送消息
            int requestTime;
            var senderRet = SendPushMsgToProvider(sendProcessDto, deviceChannelDto, out requestTime);
            stop.Stop();
            //LogHelper.Info.Write("SendNewProcess", string.Format("SendPushMsgToProvider \r\n SystemType:{0},TimeSpan:{1}", deviceChannelDto.SystemType.ToString(), stop.ElapsedMilliseconds.ToString()));
            stop.Restart();
            //执行Db操作
            SendMsgToDB(senderRet, sendProcessDto, deviceChannelDto, requestTime);
            stop.Stop();
            //LogHelper.Info.Write("SendNewProcess", string.Format("SendMsgToDB \r\n SystemType:{0},TimeSpan:{1}", deviceChannelDto.SystemType.ToString(), stop.ElapsedMilliseconds.ToString()));

            //推送成功 
            return true;
        }



        /// <summary>
        /// 批量推送消息
        /// </summary>
        /// <param name="processList">推送消息</param>
        /// <param name="channelDto">通道信息</param>
        /// <param name="retMsg"></param>
        ///<param name="sprint">3</param>
        /// <returns></returns>
        public bool SendProcessList(List<SendProcessDto> processList, out string retMsg)
        {
            Stopwatch stop = new Stopwatch();
            retMsg = string.Empty;
            if (processList == null)
            {
                retMsg = "SendProcessDtoList为空";
                return false;
            }
            //processList 必须要有值，否则报错
            var process = processList.First();
            //获取通道的信息
            long? channelId = process.ChannelId;
            if (!channelId.HasValue)
            {
                retMsg = "推送消息中，通道Id为空";
                return false;
            }
            ChannelDto channelDto;
            bool flag = _channelLogic.CheckChannel(channelId.Value, out channelDto, out retMsg);
            if (!flag)
            {
                return false;
            }
            stop.Start();
            //底层发送消息
            //分两种情况 1、IOS 消息推送 2、Android 消息推送
            int requestTime;
            //IOS逻辑
            var iosProcessList = processList.Where(t => _tokenBrandLogic.MatchSystemType(t.BrandId.Value) == (int)SystemTypeEnum.IOS).ToList();
            SenderRet iosSenderRet = SendPushMsgListToProvider(iosProcessList, SystemTypeEnum.IOS, channelDto, out requestTime);
            SendMsgListToDB(iosSenderRet, iosProcessList, requestTime);
            //Android逻辑
            var androidProcessList = processList.Where(t => _tokenBrandLogic.MatchSystemType(t.BrandId.Value) == (int)SystemTypeEnum.Android).ToList();
            SenderRet androidSenderRet = SendPushMsgListToProvider(androidProcessList, SystemTypeEnum.Android, channelDto, out requestTime);
            SendMsgListToDB(androidSenderRet, androidProcessList, requestTime);
            //推送成功 
            return true;
        }

        /// <summary>
        /// 将消息推送给推送平台
        /// </summary>
        /// <param name="list"></param>
        /// <param name="channel"></param>
        /// <param name="requestTime"></param>
        /// <returns></returns>
        public SenderRet SendPushMsgListToProvider(List<SendProcessDto> processList, SystemTypeEnum systemType, ChannelDto channelDto, out int requestTime)
        {
            string retMsg = string.Empty;
            SenderRet senderRet = new SenderRet { IsSuccess = true };
            requestTime = 0;
            if (processList == null || processList.Count == 0)
            {
                senderRet.IsSuccess = false;
                senderRet.Msg = "SendProcessDtoList为空";
                return senderRet;
            }
            int appId = processList.First().AppId;
            AppChannelDto appChannelDto;
            if (!_channelLogic.CheckAppChannel(appId, (int)systemType, channelDto.Id, out appChannelDto, out retMsg))
            {
                senderRet.IsSuccess = false;
                senderRet.Msg = retMsg;
                return senderRet;
            }
            //推送第三方平台  友盟只能单推，小米可以批量推，个推也能批量推送
            ISender sender = PushSenderManager.GetSender(channelDto.Id);
            if (sender == null)
            {

                senderRet.IsSuccess = false;
                senderRet.Msg = string.Format("ChannelId:{0},推送信息供应商不存在", channelDto.Id);
                return senderRet;
            }
            PushChannelModel pushChannelModel = new PushChannelModel
            {
                ChannelName = channelDto.ChannelName,
                Url = channelDto.MultiUrl,
                AppKey = appChannelDto.AppKey,
                AppSecret = appChannelDto.AppSecret,
                ProductionMode = Convert.ToBoolean(_configLogic.GetConfigValue(ConfigKey.ProductionMode)),
                SystemType = systemType,
                PushNum = channelDto.PushNum ?? 50,
                TimeOut = channelDto.PushTimeOut

            };
            List<PushMsgModel> pushMsgModelList = processList.Select(e =>
            {
                return new PushMsgModel
                {
                    Id = e.Id,
                    Ticker = e.Title,
                    Title = e.Title,
                    Msg = e.Msg,
                    AttachInfo = e.AttachInfo,
                    DeviceToken = e.DeviceToken,
                    SystemType = systemType
                };
            }).ToList();
            var isRealPushMsg = _configLogic.GetConfigValue(ConfigKey.IsRealPushMsg);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (isRealPushMsg != null && Convert.ToBoolean(isRealPushMsg))
            {
                senderRet = sender.SendList(pushChannelModel, pushMsgModelList);
            }
            else
            {
                Thread.Sleep(2000);
                senderRet = new SenderRet { IsSuccess = true, Sign = Guid.NewGuid().ToString() };
            }
            sw.Stop();
            requestTime = (int)sw.ElapsedMilliseconds;
            return senderRet;
        }


        /// <summary>
        /// 推送消息之后执行DB操作
        /// </summary>
        /// <param name="senderRet">发送记录</param>
        /// <param name="processList">推送消息集合</param>
        /// <param name="rzTokenBrandDto">注册信息</param>
        /// <param name="rzTokenBrandDetailDto">注册明细</param>
        /// <param name="channelDto">通道</param>
        /// <param name="requestTime">执行时间</param>
        public void SendMsgListToDB(SenderRet senderRet, List<SendProcessDto> processList, int requestTime)
        {
            if (processList == null || processList.Count == 0)
                return;
            //两个策略
            //第一如果senderRet中ResultList没有值，说明批量发送
            if (senderRet.ResultList == null || senderRet.ResultList.Count == 0)
            {
                SendMsgListBatchToDB(senderRet, processList, requestTime);
                return;
            }
            //第二如果senderRet中ResultList有值，说明是批量循环单条发送
            SendMsgListSingleToDB(senderRet, processList, requestTime);
        }

        /// <summary>
        /// 批量发送给推送平台，在返回结果中也只有批量返回的唯一标识
        /// 批量成功或批量失败
        /// </summary>
        /// <param name="senderRet"></param>
        /// <param name="processList"></param>
        /// <param name="requestTime"></param>
        public void SendMsgListBatchToDB(SenderRet senderRet, List<SendProcessDto> processList, int requestTime)
        {
            //不管发送成功或失败，入历史表
            List<ProcessHistoryDto> processHistoryDtoList = _mapper.Map<List<ProcessHistoryDto>>(processList);
            DateTime sendTime = DateTimeHelper.GetNow();
            processHistoryDtoList.ForEach(e =>
            {
                if (senderRet.IsSuccess)
                {
                    e.SendStatus = (int)SendStatusEnum.Success;
                    e.ReturnSign = senderRet.Sign;
                }
                else
                {
                    e.SendStatus = (int)SendStatusEnum.Failure;
                    e.ErrorType = (int)ErrorTypeEnum.PushPlatform;
                    e.Remark = senderRet.Code;
                }
                e.SendTime = sendTime;
                e.RequestTime = requestTime;
            });
            List<AddProcessHistoryDomainModel> modelList = _mapper.Map<List<AddProcessHistoryDomainModel>>(processHistoryDtoList);
            int rows = _pushProcessHistoryService.InsertProcessHistoryListAsync(modelList).Result;
            if (!senderRet.IsSuccess)
            {
                //发送失败的逻辑
                //sendTime 其实没有什么用，以备以后扩展;
                //以前的策略DateTime.Now.AddSeconds((sendProcessDto.EndTime - DateTime.Now).TotalSeconds / 2);只适用于单条
                //回写字段
                int writeBackRows = _pushSendProcessService.WriteBackProcessByIdsAsync(processList.Select(t => t.Id).ToList(), sendTime).Result;
                return;
            }
            //发送成功才会删除待发送列表
            List<long> ids = processList.Select(t => t.Id).ToList();
            rows = _pushSendProcessService.DeleteProcessByIdsAsync(ids).Result;
        }


        /// <summary>
        /// 批量发送给推送平台，因推送平台不支持批量推送不同的人不同的消息，内部实现中依旧是循环单条发送
        /// 分别记录发送成功的标识和发送失败的标识
        /// </summary>
        /// <param name="senderRet"></param>
        /// <param name="processList"></param>
        /// <param name="requestTime"></param>
        public void SendMsgListSingleToDB(SenderRet senderRet, List<SendProcessDto> processList, int requestTime)
        {
            //不管发送成功或失败，入历史表
            //区分发送成功的集合 和 发送失败的集合
            List<ProcessHistoryDto> processHistoryDtoList = _mapper.Map<List<ProcessHistoryDto>>(processList);
            DateTime sendTime = DateTimeHelper.GetNow();
            processHistoryDtoList.ForEach(e =>
            {
                SenderRet senderRetTemp = senderRet.ResultList.Where(t => t.Id == e.Id).FirstOrDefault();
                if (senderRetTemp == null) return;
                if (senderRetTemp.IsSuccess)
                {
                    e.SendStatus = (int)SendStatusEnum.Success;
                    e.ReturnSign = senderRetTemp.Sign;
                }
                else
                {
                    e.SendStatus = (int)SendStatusEnum.Failure;
                    e.ErrorType = (int)ErrorTypeEnum.PushPlatform;
                    e.Remark = string.IsNullOrWhiteSpace(senderRetTemp.Code) ? senderRetTemp.Msg : senderRetTemp.Code;
                }
                e.SendTime = sendTime;
                e.RequestTime = requestTime;
                e.Id = 0;//初始化为0；
            });
            List<AddProcessHistoryDomainModel> modelList = _mapper.Map<List<AddProcessHistoryDomainModel>>(processHistoryDtoList);
            int rows = _pushProcessHistoryService.InsertProcessHistoryListAsync(modelList).Result;
            List<SenderRet> failureSenderRetList = senderRet.ResultList.Where(t => t.IsSuccess == false).ToList();
            if (failureSenderRetList != null && failureSenderRetList.Count > 0)
            {
                //发送失败的逻辑
                //sendTime 其实没有什么用，以备以后扩展;
                //以前的策略DateTime.Now.AddSeconds((sendProcessDto.EndTime - DateTime.Now).TotalSeconds / 2);只适用于单条
                //回写字段
                List<long> ids = failureSenderRetList.Select(t => t.Id).ToList();
                int writeBackRows = _pushSendProcessService.WriteBackProcessByIdsAsync(ids, sendTime).Result;
            }
            List<SenderRet> successSenderRetList = senderRet.ResultList.Where(t => t.IsSuccess == true).ToList();
            //发送成功才会删除待发送列表
            if (successSenderRetList != null && successSenderRetList.Count > 0)
            {
                List<long> ids = successSenderRetList.Select(t => t.Id).ToList();
                int deleteRows = _pushSendProcessService.DeleteProcessByIdsAsync(ids).Result;
            }
        }

    }
}
