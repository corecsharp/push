using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.ChannelService.DomainModel
{
    /// <summary>
    ///  app走的推送通道
    /// </summary>
    public class PushAppChannelDomainModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 来自哪个App(配置在dic中)
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// 系统版本(0：IOS 1：Android)
        /// </summary>
        public int SystemType { get; set; }

        /// <summary>
        /// 通道Id
        /// </summary>
        public long ChannelId { get; set; }

        /// <summary>
        /// AppKey
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateAt { get; set; }

    }
}
