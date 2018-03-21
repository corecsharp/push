using Push.Service.ChannelService.DomainModel;
using Sherlock.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.ChannelService.Service
{
    /// <summary>
    /// app走的推送通道
    /// </summary>
    public interface IPushAppChannelService : IDependency
    {
        /// <summary>
        /// 取推送App的通道
        /// </summary>
        /// <param name="appId">app编号</param>
        /// <param name="systemType">系统类型（0：IOS 1:Android）</param>
        /// <param name="channelId">推送通道编号</param>
        /// <returns></returns>
        Task<PushAppChannelDomainModel> GetAppChannelByAppInfoAsync(int appId, int systemType, long channelId);

    }
}
