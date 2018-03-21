using Push.Service.ChannelService.DomainModel;
using Sherlock.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.ChannelService.Service
{
    /// <summary>
    /// 推送通道
    /// </summary>
    public interface IPushChannelService : IDependency
    {
        /// <summary>
        /// 根据id获取推送通道
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PushChannelDomainModel> GetChannelByIdAsync(long id);

    }
}
