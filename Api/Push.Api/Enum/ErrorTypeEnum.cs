using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.Enum
{
    /// <summary>
    /// 发送失败，错误类型
    /// </summary>
   public enum ErrorTypeEnum
    {
        /// <summary>
        /// 尝试发送次数过多
        /// </summary>
        [Description("尝试发送次数过多")]
        TryTimesOver=1,
        /// <summary>
        /// 超时
        /// </summary>
        [Description("超时")]
        TimeOut = 2,
        /// <summary>
        /// 账号登出
        /// </summary>
        [Description("账号登出")]
        Logout = 3,
        /// <summary>
        /// 配置错误
        /// </summary>
        [Description("配置信息错误")]
        ConfigError = 4,
        /// <summary>
        /// 发送到推送平台失败
        /// </summary>
        [Description("发送到推送平台失败")]
        PushPlatform = 5,


    }
}
