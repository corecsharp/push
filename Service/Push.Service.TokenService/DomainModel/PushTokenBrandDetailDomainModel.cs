using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.TokenService.DomainModel
{
    /// <summary>
    ///  注册信息明细
    /// </summary>
    public class PushTokenBrandDetailDomainModel
    {
        /// <summary>
        /// TokenBrandId
        /// </summary>
        public long TokenBrandId { get; set; }

        /// <summary>
        /// 消息推送服务对设备的唯一标识（第三方标识）
        /// </summary>
        public string DeviceToken { get; set; }

        /// <summary>
        /// 通道Id
        /// </summary>
        public long ChannelId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateAt { get; set; }

    }
}
