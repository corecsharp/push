using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.Enum.UM
{
    /// <summary>
    /// 点击"通知"的后续行为
    /// </summary>
    public enum AfterOpenAction
    {
        /// <summary>
        /// 打开应用
        /// </summary>
        go_app,
        /// <summary>
        /// 跳转到URL
        /// </summary>
        go_url,
        /// <summary>
        /// 打开特定的activity
        /// </summary>
        go_activity,
        /// <summary>
        /// 用户自定义内容
        /// </summary>
        go_custom
    }
}
