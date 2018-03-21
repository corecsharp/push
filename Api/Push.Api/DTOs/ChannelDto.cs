using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.DTOs
{
    /// <summary>
    /// 推送通道
    /// </summary>
    public class ChannelDto : CourseBaseDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 通道路劲
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 批量推送通道路劲
        /// </summary>
        public string MultiUrl { get; set; }

        /// <summary>
        /// 通道状态：有效或无效
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 推送数量
        /// </summary>
        public int? PushNum { get; set; }

        /// <summary>
        /// 推送超时时间(秒)
        /// </summary>
        public int PushTimeOut { get; set; }
    }
}
