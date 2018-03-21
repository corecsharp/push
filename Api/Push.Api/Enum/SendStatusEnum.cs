using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.Enum
{
   public enum SendStatusEnum
    {
        /// <summary>
        /// 发送失败
        /// </summary>
        [Description("发送失败")]
        Failure = 0,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success=1
    }
}
