using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.Enum
{
    public enum ErrorCode
    {

        [Description("失败")]
        Failure = 40000,

        [Description("未找到信息")]
        NotFoud = 40004,

        [Description("成功")]
        Success = 10000,
    }
}
