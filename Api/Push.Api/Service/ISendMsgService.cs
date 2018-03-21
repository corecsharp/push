using Push.Api;
using Push.Api.DTOs;
using Push.Api.Enum;
using Sherlock.Framework;
using System.Collections.Generic;

namespace Push.Api.Service
{
    /// <summary>
    /// 推送消息Service
    /// </summary>
    public interface ISendMsgService : IDependency
    {
        /// <summary>
        /// 轮询推送消息
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        ErrCode SendMsg(long processId, out string ret);

        /// <summary>
        /// 轮询推送消息
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        ErrCode SendMsgListByBatchNO(string batchNO, out string ret);


        /// <summary>
        /// 分类，分情况批处理
        /// </summary>
        /// <param name="processList"></param>
        /// <param name="classify"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        bool HandleList(List<SendProcessDto> processList, ClassifyEnum classify, out string ret);
    }
}
