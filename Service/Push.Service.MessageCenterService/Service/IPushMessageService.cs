using Push.Service.MessageCenterService.DomainModel;
using Sherlock.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.MessageCenterService.Service
{
    public interface IPushMessageService : IDependency
    {
      
        /// <summary>
        /// 读取消息
        /// </summary>
        /// <param name="id">消息主键</param>
        /// <returns></returns>
        Task<int> ReadPushMessageAsync(long id,long userId,int appId);

        /// <summary>
        /// 批量设置消息阅读状态为已读。
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="Operator"></param>
        /// <returns></returns>
        Task<int> BatchSetReadAsync(long[] ids,long userId,int appId);

        /// <summary>
        /// 清空消息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        Task<int> ClearPushMessageAsync(long userId,int appId);

        /// <summary>
        /// 未读消息个数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> UnReadCountAsync(long userId,int appId);

        /// <summary>
        /// 获取消息明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PushMessageDomainModel> GetPushMessageAsync(long id,long userId,int appId);

        /// <summary>
        /// 通过用户id和消息类型获取消息列表
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="messageType">消息类型</param>
        /// <returns></returns>
        Task<IList<PushMessageDomainModel>> GetPushMessageListByMessageTypeAsync(long userId, int appId, int messageType);

        /// <summary>
        /// 通过用户id获取消息列表分类，自带最新头一个消息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="messageType">消息类型</param>
        /// <returns></returns>
        Task<IList<PushMessageTypeDomainModel>> GetPushMessageListGroupByMessageTypeAsync(long userId,int appId);




    }
}
