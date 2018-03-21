using Push.Api.DTOs;
using Push.Api.Enum;
using Push.Core.DTOs;
using Push.Core.Infrastructure;
using Sherlock.Framework;
using System.Collections.Generic;

namespace Push.Api.Logic
{
    /// <summary>
    /// 推送消息的逻辑
    /// </summary>
    public interface ISendProcessLogic:IDependency
    {

        /// <summary>
        /// 备案错误推送消息
        /// </summary>
        /// <param name="sendProcessDto">推送消息</param>
        /// <param name="errorStatus">错误类型</param>
        /// <param name="errorRemark">错误信息</param>
        /// <returns></returns>
        void RecordErrorProcess(SendProcessDto sendProcessDto, ErrorTypeEnum errorType, string errorRemark);


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
        SenderRet SendPushMsgToProvider(SendProcessDto sendProcessDto, DeviceChannelDto deviceChannelDto, out int requestTime);


        /// <summary>
        /// 推送消息之后执行DB操作
        /// </summary>
        /// <param name="senderRet">发送记录</param>
        /// <param name="sendProcessDto">推送消息</param>
        /// <param name="rzTokenBrandDto">注册信息</param>
        /// <param name="rzTokenBrandDetailDto">注册明细</param>
        /// <param name="channelDto">通道</param>
        /// <param name="requestTime">执行时间</param>
        void SendMsgToDB(SenderRet senderRet, SendProcessDto sendProcessDto, DeviceChannelDto deviceChannelDto, int requestTime);

        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="sendProcessDto">推送消息</param>
        /// <param name="rzTokenBrandDto">注册信息</param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        bool SendNewProcess(SendProcessDto sendProcessDto, DeviceChannelDto deviceChannelDto, out string retMsg);

        /// <summary>
        /// 批量推送消息
        /// </summary>
        /// <param name="processList">推送消息</param>
        /// <param name="channelDto">通道信息</param>
        /// <param name="retMsg"></param>
        ///<param name="sprint">3</param>
        /// <returns></returns>
        bool SendProcessList(List<SendProcessDto> processList, out string retMsg);


        /// <summary>
        /// 将消息推送给推送平台
        /// </summary>
        /// <param name="list"></param>
        /// <param name="channel"></param>
        /// <param name="requestTime"></param>
        /// <returns></returns>
        SenderRet SendPushMsgListToProvider(List<SendProcessDto> processList, SystemTypeEnum systemType, ChannelDto channelDto, out int requestTime);
        /// <summary>
        /// 推送消息之后执行DB操作
        /// </summary>
        /// <param name="senderRet">发送记录</param>
        /// <param name="processList">推送消息集合</param>
        /// <param name="rzTokenBrandDto">注册信息</param>
        /// <param name="rzTokenBrandDetailDto">注册明细</param>
        /// <param name="channelDto">通道</param>
        /// <param name="requestTime">执行时间</param>
        void SendMsgListToDB(SenderRet senderRet, List<SendProcessDto> processList, int requestTime);

        /// <summary>
        /// 批量发送给推送平台，在返回结果中也只有批量返回的唯一标识
        /// 批量成功或批量失败
        /// </summary>
        /// <param name="senderRet"></param>
        /// <param name="processList"></param>
        /// <param name="requestTime"></param>
        void SendMsgListBatchToDB(SenderRet senderRet, List<SendProcessDto> processList, int requestTime);

        /// <summary>
        /// 批量发送给推送平台，因推送平台不支持批量推送不同的人不同的消息，内部实现中依旧是循环单条发送
        /// 分别记录发送成功的标识和发送失败的标识
        /// </summary>
        /// <param name="senderRet"></param>
        /// <param name="processList"></param>
        /// <param name="requestTime"></param>
        void SendMsgListSingleToDB(SenderRet senderRet, List<SendProcessDto> processList, int requestTime);
    }
}
