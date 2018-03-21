using Push.Api;
using Push.Api.DTOs;
using Sherlock.Framework;
using System.Collections.Generic;

namespace Push.Api.Service
{

    public interface IMsgService : IDependency
    {
      
        /// <summary>
        /// 接收外部推送消息，如果时效时间为空。默认24小时
        /// </summary>
        /// <param name="model"></param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        ErrCode AcceptMsgList(SendMsgListRequest model, out string retMsg);

        /// <summary>
        /// 接受外部推送消息，推送全部用户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        ErrCode AcceptAllMsgList(SendMsgListRequest model, out string retMsg);

        ErrCode CheckAcceptMsg(SendMsgDto model, out string retMsg);
        /// <summary>
        /// 接收外部推送消息,直接通知产线已经接受成功
        /// 处理数据丢到线程池中进行
        /// </summary>
        /// <param name="model"></param>
        /// <param name="retMsg"></param>
        /// <param name="sprint">3</param>
        /// <returns></returns>
        ErrCode AcceptMsgToTheardPool(SendMsgRequest model, out string retMsg);

        /// <summary>
        /// 接收外部推送消息,直接通知产线已经接受成功
        /// 处理数据丢到线程池中进行
        /// </summary>
        /// <param name="model"></param>
        /// <param name="retMsg"></param>
        /// <param name="sprint">3</param>
        /// <returns></returns>
        ErrCode AcceptMsgListToTheardPool(List<SendMsgRequest> model, out string retMsg);
        /// <summary>
        /// 将队列中的推送消息，丢到程序池中处理
        /// </summary>
        void HandleMsgToTreadPool();
        /// <summary>
        /// 推送队列长度
        /// </summary>
        /// <returns></returns>
        int GetQueueLength();

    }
}
