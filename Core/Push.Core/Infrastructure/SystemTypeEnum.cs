using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.Infrastructure
{
    /// <summary>
    /// 移动设备类型
    /// </summary>
    public enum SystemTypeEnum
    {
        /// <summary>
        /// IOS系统
        /// </summary>
        [Description("IOS系统")]
        IOS = 0,

        /// <summary>
        /// Android系统
        /// </summary>
        [Description("Android系统")]
        Android = 1
    }
}
