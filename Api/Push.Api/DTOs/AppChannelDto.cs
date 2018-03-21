using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.DTOs
{
    /// <summary>
    ///  App和通道的关系
    /// </summary>
    public class AppChannelDto : CourseBaseDto
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
    }
}
