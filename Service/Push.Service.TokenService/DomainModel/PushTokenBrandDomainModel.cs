using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.TokenService.DomainModel
{
    /// <summary>
    ///  手机设备注册
    /// </summary>
    public class PushTokenBrandDomainModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 目前使用userid
        /// </summary>
        public string RzToken { get; set; }

        /// <summary>
        /// 来自哪个App(配置在dic中)
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// 手机唯一标识
        /// </summary>
        public virtual string DeviceId { get; set; }

        /// <summary>
        /// 品牌编号（品牌存于dic表中）
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// 手机系统(0：IOS 1：Android)
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

    }
}
