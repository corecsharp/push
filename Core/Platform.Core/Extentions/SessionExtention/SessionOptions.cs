using Sherlock.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Core.Extentions
{
    [ConfiguredOptions(nameof(GZSessionOptions))]
    public class GZSessionOptions
    {
        /// <summary>
        /// 有效时间（单位分钟）
        /// </summary>
        public int Duration { get; set; } = 30;

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 是否使用滑动过期
        /// </summary>
        public bool Sliding { get; set; } = true;
    }
}
