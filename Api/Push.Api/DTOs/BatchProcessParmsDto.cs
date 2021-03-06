﻿using System;

namespace Push.Api.DTOs
{
    /// <summary>
    /// 获取批次必要的参数（sprint3）
    /// </summary>
    public class BatchProcessParmsDto
    {
        /// <summary>
        /// Top行数
        /// </summary>
        public int TopNum { get; set; }
        /// <summary>
        /// 应用AppId
        /// </summary>
        public long AppId { get; set; }
        /// <summary>
        /// 通道Id
        /// </summary>
        public long ChannelId { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public Guid BatchNo { get; set; }
        /// <summary>
        /// 现在时间
        /// </summary>
        public DateTime TimeNow { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public DateTime ExpireTime { get; set; }

    }
}
