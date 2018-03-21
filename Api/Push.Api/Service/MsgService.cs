using AutoMapper;
using Platform.Core.Helper;
using Platform.Core.Options;
using Push.Api;
using Push.Api.DTOs;
using Push.Api.Enum;
using Push.Api.Logic;
using Push.Service.MessageCenterService.DomainModel;
using Push.Service.ProcessService.DomainModel;
using Push.Service.ProcessService.Service;
using Push.Service.TokenService.DomainModel;
using Push.Service.TokenService.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sherlock.Framework.Environment;
using Sherlock.Framework.Events;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Push.Api.Service
{

    public class MsgService : IMsgService
    {
        //静态锁，只被初始化一次
        private static object queueLock = new object();
        private static object semaphoreLock = new object();
        //信号量
        private static Semaphore _semaphore;
        private const int Semaphore_InitCount = 4;//信号量默认给个4
        private static int SemaphoreCount = Semaphore_InitCount;//全局信号量变量
        private static IQueueLogic<SendMsgRequest> queueLogic;
        //初始化服务
        private IConfigLogic _configLogic;
        private IMapper _mapper;
        private ITokenBrandLogic _tokenBrandLogic;
        private IPushTokenBrandService _pushTokenBrandService;
        private ISendProcessLogic _sendProcessLogic;
        private IPushSendProcessService _pushSendProcessService;
        private IEventNotification _eventNotification;
        //private ILoggerFactory _loggerFactory;

        public MsgService(IConfigLogic configLogic
            , IMapper mapper
            , ITokenBrandLogic tokenBrandLogic
            , IPushTokenBrandService pushTokenBrandService
            , ISendProcessLogic sendProcessLogic
            , IPushSendProcessService pushSendProcessService
            , IEventNotification eventNotification
            //, ILoggerFactory loggerFactory
            )
        {
            _configLogic = configLogic;
            _mapper = mapper;
            _tokenBrandLogic = tokenBrandLogic;
            _pushTokenBrandService = pushTokenBrandService;
            _sendProcessLogic = sendProcessLogic;
            _pushSendProcessService = pushSendProcessService;
            _eventNotification = eventNotification;
            //_loggerFactory = loggerFactory;
            //初始化信号量
            InitSemaphore();
            //初始化队列
            InitQueue();
        }

        /// <summary>
        /// 接收外部推送消息，如果时效时间为空。默认24小时
        /// </summary>
        /// <param name="model"></param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        public ErrCode AcceptMsgList(SendMsgListRequest model, out string retMsg)
        {
            retMsg = string.Empty;
            ErrCode errorCode = CheckAcceptMsg(model, out retMsg);
            if (errorCode != ErrCode.Sucess) return errorCode;
            int length = model.TokenList.Count;
            //判断接收人的个数是否超出上限
            var tokenMaxNum = Convert.ToInt32(_configLogic.GetConfigValue(ConfigKey.TokenMaxNum));
            if (length > tokenMaxNum)
            {
                retMsg = string.Format("Token的个数太多，网关限制上限：{0}", tokenMaxNum);
                return ErrCode.Failure;
            }
            List<SendMsgRequest> sendMsgRequestList = new List<SendMsgRequest>();
            foreach (var item in model.TokenList)
            {
                SendMsgRequest sendMsgRequest = _mapper.Map<SendMsgRequest>(model);
                sendMsgRequest.Token = item;
                sendMsgRequestList.Add(sendMsgRequest);
            }
            AcceptMsgListToTheardPool(sendMsgRequestList, out retMsg);
            return ErrCode.Sucess;
        }

        /// <summary>
        /// 接受外部推送消息，推送全部用户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        public ErrCode AcceptAllMsgList(SendMsgListRequest model, out string retMsg)
        {
            retMsg = string.Empty;
            ErrCode errorCode = CheckAcceptMsg(model, out retMsg);
            if (errorCode != ErrCode.Sucess) return errorCode;
            var tokenList = model.TokenList;
            //判断接收人的个数是否超出上限
            var tokenMaxNum = Convert.ToInt32(_configLogic.GetConfigValue(ConfigKey.TokenMaxNum));
            while (tokenList.Count > tokenMaxNum)
            {
                var partTokenList = tokenList.GetRange(0, tokenMaxNum);
                MsgToTheardPoolByTokenList(partTokenList, model, out retMsg);
                tokenList.RemoveRange(0, tokenMaxNum);
            }
            MsgToTheardPoolByTokenList(tokenList, model, out retMsg);
            return ErrCode.Sucess;
        }

        public void MsgToTheardPoolByTokenList(List<string> tokenList, SendMsgListRequest model, out string retMsg)
        {
            List<SendMsgRequest> sendMsgRequestList = new List<SendMsgRequest>();
            foreach (var item in tokenList)
            {
                SendMsgRequest sendMsgRequest = _mapper.Map<SendMsgRequest>(model);
                sendMsgRequest.Token = item;
                sendMsgRequestList.Add(sendMsgRequest);
            }
            AcceptMsgListToTheardPool(sendMsgRequestList, out retMsg);
        }


        /// <summary>
        /// 将sendMsgDto转为SendMsgToSendProcess，并赋值参数
        /// </summary>
        /// <param name="sendMsgDto"></param>
        /// <returns></returns>
        private SendProcessDto SendMsgToSendProcess(SendMsgDto sendMsgDto, string serialNO)
        {
            sendMsgDto.StartTime = sendMsgDto.StartTime.HasValue ? sendMsgDto.StartTime : DateTimeHelper.GetNow();
            SendProcessDto sendProcessDto = _mapper.Map<SendProcessDto>(sendMsgDto);
            sendProcessDto.AttachInfo = sendMsgDto.AttachInfo == null ? null : JsonConvert.SerializeObject(sendMsgDto.AttachInfo);
            sendProcessDto.SerialNo = serialNO;
            sendProcessDto.PriorityLevel = (int)PriorityLevelEnum.Middle;//默认优先级为中
            sendProcessDto.StartTime = sendMsgDto.StartTime.Value;
            sendProcessDto.EndTime = sendMsgDto.Timeliness.HasValue ? sendProcessDto.StartTime.AddMinutes(sendMsgDto.Timeliness.Value) : sendProcessDto.StartTime.AddMinutes(24 * 60);
            sendProcessDto.IsUsed = false;
            sendProcessDto.ExpireTime = sendProcessDto.StartTime;
            sendProcessDto.SendTime = sendProcessDto.StartTime;
            sendProcessDto.DelayTimes = 0;
            return sendProcessDto;
        }

        public ErrCode CheckAcceptMsg(SendMsgDto model, out string retMsg)
        {

            retMsg = string.Empty;
            if (model == null)
            {
                retMsg = "推送消息为空";
                return ErrCode.Failure;
            }
            if (model.Timeliness.HasValue && model.Timeliness <= 0)
            {
                retMsg = "时效时间（分钟）必须大于0";
                return ErrCode.Failure;
            }
            //检查AppId是否存在
            if (!_tokenBrandLogic.CheckAppId(model.AppId, out retMsg))
            {
                return ErrCode.AppNotExist;
            }
            return ErrCode.Sucess;
        }

        /// <summary>
        /// 接收外部推送消息,直接通知产线已经接受成功
        /// 处理数据丢到线程池中进行
        /// </summary>
        /// <param name="model"></param>
        /// <param name="retMsg"></param>
        /// <param name="sprint">3</param>
        /// <returns></returns>
        public ErrCode AcceptMsgToTheardPool(SendMsgRequest model, out string retMsg)
        {

            retMsg = string.Empty;
            ErrCode errorCode = CheckAcceptMsg(model, out retMsg);
            if (errorCode != ErrCode.Sucess) return errorCode;
            //丢到队列中,使用新的线程，将数据丢到线程池中
            //_msgQueue.Enqueue(model);//使用队列存储数据，使用信号量控制线程中量。开启一个新的后台线程，将数据丢到线程池中
            queueLogic.Enqueue(model);
            return ErrCode.Sucess;
        }


        /// <summary>
        /// 接收外部推送消息,直接通知产线已经接受成功
        /// 处理数据丢到线程池中进行
        /// </summary>
        /// <param name="model"></param>
        /// <param name="retMsg"></param>
        /// <param name="sprint">3</param>
        /// <returns></returns>
        public ErrCode AcceptMsgListToTheardPool(List<SendMsgRequest> model, out string retMsg)
        {
            retMsg = string.Empty;
            queueLogic.Enqueue(model);
            return ErrCode.Sucess;
        }

        /// <summary>
        /// 处理数据传给DB
        /// 将消息一拆为n个消息
        /// </summary>
        /// <param name="model"></param>
        private void HandleMsgToDB(object model)
        {
            //var _logger = _loggerFactory.CreateLogger<MsgService>();
            using (var scope = SherlockEngine.Current.CreateScope())
            {
                try
                {
                    string serialNO = Guid.NewGuid().ToString();//同一条消息进行拆解
                    SendMsgRequest sendMsgRequest = (SendMsgRequest)model;
                    //将推送消息推送消息中心
                    PushMessageToMessageCenter(sendMsgRequest);
                    //拆分数据
                    //获取设备和通道
                    IPushTokenBrandService pushTokenBrandService = scope.ServiceProvider.GetService(typeof(IPushTokenBrandService)) as IPushTokenBrandService;
                    List<DeviceChannelDomainModel> deviceChannelDomainModelList = pushTokenBrandService.GetDeviceChannelListAsync(sendMsgRequest.Token, sendMsgRequest.AppId).Result;
                    List<DeviceChannelDto> deviceChannelList = _mapper.Map<List<DeviceChannelDto>>(deviceChannelDomainModelList);
                    //未获取到，接收的消息直接进历史表，标记为账号登出
                    if (deviceChannelList == null || deviceChannelList.Count == 0)
                    {
                        //直接入历史表，标记错误
                        SendProcessDto sendProcessDto = SendMsgToSendProcess(sendMsgRequest, serialNO);
                        sendProcessDto.Token = sendMsgRequest.Token;
                        ISendProcessLogic sendProcessLogic = scope.ServiceProvider.GetService(typeof(ISendProcessLogic)) as ISendProcessLogic;
                        sendProcessLogic.RecordErrorProcess(sendProcessDto, ErrorTypeEnum.Logout, string.Empty);
                        return;
                    }
                    //获取到，根据设备Token拆分数据
                    List<SendProcessDto> sendProcessDtoList = new List<SendProcessDto>();
                    foreach (var item in deviceChannelList)
                    {
                        SendProcessDto sendProcessDto = SendMsgToSendProcess(sendMsgRequest, serialNO);
                        sendProcessDto.Token = sendMsgRequest.Token;
                        //将获取到的TokenBrandId(核心),BrandId,ChannelId,DeviceToken 赋给对象，可做到新老接口兼容
                        //老接口发送数据时，可根据TokenBrandId直接找到需要发送的ChannelId和DeviceToken
                        //新接口发送数据时，通过ChannelId,DeviceToken 发送数据
                        sendProcessDto.TokenBrandId = item.Id;
                        sendProcessDto.BrandId = item.BrandId;
                        sendProcessDto.ChannelId = item.ChannelId;
                        sendProcessDto.DeviceToken = item.DeviceToken;
                        sendProcessDtoList.Add(sendProcessDto);
                    }
                    IMapper mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                    List<PushSendProcessDomainModel> domainList = mapper.Map<List<PushSendProcessDomainModel>>(sendProcessDtoList);
                    IPushSendProcessService pushSendProcessService = scope.ServiceProvider.GetService(typeof(IPushSendProcessService)) as IPushSendProcessService;
                    int rows = pushSendProcessService.InsertProcessListAsync(domainList).Result;
                }
                catch (Exception ex)
                {
                    //_logger.LogError(ex.Message.ToString());
                    //LogHelper.Error.Write("HandleMsgToDB", ex);
                }
                finally
                {
                    _semaphore.Release();
                    //LogHelper.Info.Write("HandleMsgToDB", "信号量释放之前使用的数量：" + count);
                }
            }
                
        }

        /// <summary>
        /// 获取配置中信号量
        /// </summary>
        /// <returns></returns>
        private int GetConfigSemaphoreCount()
        {
            var semaphoreCount = _configLogic.GetConfigValue(ConfigKey.SemaphoreCount);
            int count;
            if (semaphoreCount == null || !int.TryParse(semaphoreCount.ToString(), out count))
            {
                //如果没有配置或配置错误给一个初始的信号量
                count = Semaphore_InitCount;
            }
            return count;
        }
        static int count = 0;
        /// <summary>
        /// 将队列中的推送消息，丢到程序池中处理
        /// </summary>
        public void HandleMsgToTreadPool()
        {
            while (true)
            {
                //检查信号量是否改变
                CheckSemaphoreCountChange();
                SendMsgRequest sendMsgRequest = queueLogic.Dequeue();
                if (sendMsgRequest != null)
                {
                    try
                    {
                        _semaphore.WaitOne();
                        //HandleMsgToDB(sendMsgRequest);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(HandleMsgToDB), sendMsgRequest);
                        //LogHelper.Info.Write("HandleMsgToTreadPool", "已加入线程池1条数据");
                    }
                    catch (Exception ex)
                    {
                        //LogHelper.Info.Write("HandleMsgToTreadPool", ex);
                        continue;
                    }
                }
                else
                {
                    //LogHelper.Info.Write("HandleMsgToTreadPool", "接收信息的队列中没有数据处理，等待1000ms");
                    Thread.Sleep(1000);
                }
            }
        }
        /// <summary>
        /// 检查信号量是否有修改
        /// </summary>
        private void CheckSemaphoreCountChange()
        {
            var configSemaphoreCount = GetConfigSemaphoreCount();//获取信号量
            if (configSemaphoreCount != SemaphoreCount)
            {
                try
                {
                    if (configSemaphoreCount > SemaphoreCount)
                    {
                        //信号量的数量变大了
                        _semaphore.Release(configSemaphoreCount - SemaphoreCount);
                    }
                    else
                    {
                        //信号量的数量变小了
                        for (int i = 0; i < SemaphoreCount - configSemaphoreCount; i++)
                        {
                            _semaphore.WaitOne();//减少信号量的个数
                        }
                    }
                    //如果信号量有修改
                    SemaphoreCount = configSemaphoreCount;
                    //LogHelper.Info.Write("CheckSemaphoreCountChange", "信号量改为：" + configSemaphoreCount);

                }
                catch (Exception ex)
                {
                    //LogHelper.Error.Write("CheckSemaphoreCountChange", ex);
                }
            }

        }

        /// <summary>
        /// 推送队列长度
        /// </summary>
        /// <returns></returns>
        public int GetQueueLength()
        {
            return queueLogic.Length();
        }

        private void InitSemaphore()
        {
            //读取配置获取信号量
            if (_semaphore == null)
            {
                lock (semaphoreLock)
                {
                    if (_semaphore == null)
                    {
                        int initialSemaphoreCount = GetConfigSemaphoreCount();
                        SemaphoreCount = initialSemaphoreCount;
                        _semaphore = new Semaphore(initialSemaphoreCount, 50);
                    }
                }
            }
        }

        private void InitQueue()
        {
            if (queueLogic == null)
            {
                lock (queueLock)
                {
                    if (queueLogic == null)
                    {
                        var redisQueueLogic = new RedisQueueLogic<SendMsgRequest>();
                        if (redisQueueLogic.IsUseRedisQueue)
                        {
                            queueLogic = redisQueueLogic;
                        }
                        else
                        {
                            queueLogic = new LocalQueueLogic<SendMsgRequest>();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 将消息推送给消息中心
        /// </summary>
        /// <param name="model"></param>
        private void PushMessageToMessageCenter(SendMsgRequest sendMsgRequest)
        {

            PushMessageDomainModel pushMessageDomainModel = _mapper.Map<PushMessageDomainModel>(sendMsgRequest);
            _eventNotification.Notify("AddMessageToMessageCenter", null, pushMessageDomainModel);
        }
    }
}
