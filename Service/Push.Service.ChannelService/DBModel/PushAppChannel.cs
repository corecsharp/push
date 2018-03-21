using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.ChannelService.DBModel
{
    /// <summary>
    /// app走的推送通道
    /// </summary>
    public class PushAppChannel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// app主键id
        /// </summary>
        public long AppId { get; set; }

        /// <summary>
        /// 系统类别
        /// </summary>
        public int SystemType { get; set; }

        /// <summary>
        /// 通道主键id
        /// </summary>
        public long ChannelId { get; set; }

        /// <summary>
        /// 程序秘钥
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 程序秘钥私钥
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateAt { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreateId { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public long UpdateId { get; set; }


    }
}
