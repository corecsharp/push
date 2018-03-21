using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.ProcessService.DomainModel
{
    /// <summary>
    /// 待推送消息中的AppId和ChannnelId
    /// </summary>
    public class PushSendProcessAppChannelDomainModel
    {
        /// <summary>
        /// app主键id
        /// </summary>
        public long AppId { get; set; }

        /// <summary>
        /// 通道主键id
        /// </summary>
        public long ChannelId { get; set; }
    }
}
