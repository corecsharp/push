using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Core
{
    public enum UserTypeEnum
    {
        /// <summary>
        /// 普通用户
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 地勤用户
        /// </summary>
        Internal = 1,
    }
    //[Flags]
    public enum UserTypeCheck
    {
        Normal= 0,
        Internal = 1,
        All = 2,
    }
}
