using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.Enum
{
    /// <summary>
    /// 优先级
    /// </summary>
    public enum PriorityLevelEnum
    {
        /// <summary>
        /// 紧急型
        /// </summary>
        [Description("紧急型")]
        High = 1,
        /// <summary>
        /// 中优先级
        /// </summary>
        [Description("中优先级")]
        Middle = 2,
        /// <summary>
        /// 低优先级
        /// </summary>
        [Description("低优先级")]
        Low = 3
    }
}
