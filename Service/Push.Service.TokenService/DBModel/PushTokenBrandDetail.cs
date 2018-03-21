using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.TokenService.DBModel
{
    /// <summary>
    ///  手机设备注册明细，包括几个推送平台的token
    /// </summary>
    public class PushTokenBrandDetail
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 外键
        /// 对应PushTokenBrand中的Id
        /// </summary>
        public long TokenBrandId { get; set; }

        /// <summary>
        /// 各个推送平台的devicetoken
        /// </summary>
        public string DeviceToken { get; set; }

        /// <summary>
        /// 通道id
        /// </summary>
        public long ChannelId { get; set; }

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
