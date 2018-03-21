using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.DTOs
{
    /// <summary>
    /// 注册信息明细
    /// </summary>
    public class TokenBrandDetailDto:CourseBaseDto
    {
        /// <summary>
        /// RzTokenBrandId
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

    }
}
