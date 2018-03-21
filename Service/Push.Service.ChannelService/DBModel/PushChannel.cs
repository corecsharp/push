using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.ChannelService.DBModel
{
    /// <summary>
    /// 推送通道
    /// </summary>
    public class PushChannel
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 单条推送URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 批量推送URL
        /// </summary>
        public string MultiUrl { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// 一次性推送数量（批量推送有用）
        /// </summary>
        public int PushNum { get; set; }

        /// <summary>
        /// 推送超时值
        /// </summary>
        public int PushTimeOut { get; set; }

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
