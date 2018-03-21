using System.ComponentModel;
using System;

namespace Platform.Core
{
    /// <summary>
    /// 错误码：这里只可以写通用错误码，具体业务错误码请写在各自模块中
    /// B2B.App.UserApi：2000-2999
    /// </summary>
    public enum ErrCodeCommon:long
    {
        /// <summary>
        /// 接口调用失败
        /// </summary>
        #region
        [Description("调用失败")]
        Failure = 1000,
        /// <summary>
        /// 无权限
        /// </summary>
        [Description("无权限")]
        PermissionDenied = 1001,
        /// <summary>
        /// 关联关系错误
        /// </summary>
        [Description("关联关系错误")]
        RelativeError = 1002,
        /// <summary>
        /// 数据已存在
        /// </summary>
        [Description("数据已存在")]
        DataIsExist = 1003,
        /// <summary>
        /// 数据不存在
        /// </summary>
        [Description("数据不存在")]
        DataIsnotExist = 1004,
        /// <summary>
        /// 数据查询错误
        /// </summary>
        [Description("数据查询错误")]
        QueryError = 1005,
        /// <summary>
        /// 数据插入错误
        /// </summary>
        [Description("数据插入错误")]
        InsertError = 1006,
        /// <summary>
        /// 数据删除错误
        /// </summary>
        [Description("数据删除错误")]
        DeleteError = 1007,
        /// <summary>
        /// 数据修改错误
        /// </summary>
        [Description("数据修改错误")]
        UpdateError = 1008,
        /// <summary>
        /// 参数错误
        /// </summary>
        [Description("参数错误")]
        ParameterError = 1009,
        /// <summary>
        /// 内部错误
        /// </summary>
        [Description("内部错误")]
        InnerError = 1010,
        /// <summary>
        /// 通用:不可为空
        /// </summary>
        [Description("不可为空")]
        NotAllowedNull = 1011,
        /// <summary>
        /// 通用:不在范围内
        /// </summary>
        [Description("不在范围内")]
        OutOfRange = 1012,
        /// <summary>
        /// 通用:不可重复
        /// </summary>
        [Description("不可重复")]
        NotAlloweDuplicate = 1013,
        /// <summary>
        /// 手机号码不合法
        /// </summary>
        [Description("手机号码不合法")]
        InvalidPhoneNum = 1014,

        [Description("输入的验证码有误～")]
        ErrSmsCode = 1015,

        [Description("密码错误")]
        PasswordErr = 1016,

        [Description("未登录")]
        NoLogin = 1017,

        [Description("验证码不存在，请重新获取～")]
        ErrSmsCodeExpire = 1018,

        [Description("验证码验证超过错误次数～")]
        ErrSmsCodeMaxLimit = 1019
        #endregion


    }


    public class ErrDetail
    {
        public string Code { get; set; }

        public string Msg { get; set; }
    }
}
