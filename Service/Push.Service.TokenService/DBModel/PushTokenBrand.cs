using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.TokenService.DBModel
{
    /// <summary>
    /// 手机设备注册
    /// </summary>
    public class PushTokenBrand
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// app主键id
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// 设备id
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 品牌id
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// 系统类别
        /// </summary>
        public int SystemType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateAt { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreateId { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public long UpdateId { get; set; }


    }
}
