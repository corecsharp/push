using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.Enum
{
    /// <summary>
    /// 分类
    /// </summary>
    public enum ClassifyEnum
    {
        /// <summary>
        /// 超时
        /// </summary>
        [Description("超时")]
        Timeout = 1,
        /// <summary>
        /// 重试次数过多
        /// </summary>
        [Description("重试次数过多")]
        RetryTimesOver = 2,
        /// <summary>
        /// 正常发送
        /// </summary>
        [Description("正常发送")]
        Send = 3,
    }
}
