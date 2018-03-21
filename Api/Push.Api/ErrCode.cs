using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Push.Api
{
    public enum ErrCode:long
    {
        Failure = 9000,
        PermissionDenied = 9990,
        RelativeError = 9991,
        DataIsExist = 9992,
        //[Description("数据不存在")]
        DataIsnotExist = 9993,
        QueryError = 9994,
        InsertError = 9995,
        DeleteError = 9996,
        UpdateError = 9997,
        ParameterError = 9998,
        InnerError = 9999,
        Sucess = 10000,

        /// <summary>
        /// App不存在。
        /// </summary>
        [Description("指定的APP不存在")]
        AppNotExist = 10010,

        InvalidPhoneNum = 10005,
        //
        // 摘要:
        //     通用:{0}不可为空
        NotAllowedNull = 300000,
        //
        // 摘要:
        //     通用:{0}不在范围内
        OutOfRange = 300001,
        NotAlloweDuplicate = 300002
    }
}
