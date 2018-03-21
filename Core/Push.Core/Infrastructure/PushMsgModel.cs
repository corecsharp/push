using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.Infrastructure
{
    /// <summary>
    /// 推送消息实体
    /// </summary>
    public class PushMsgModel
    {
        /// <summary>
        /// 单条的消息Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 通知栏提示文字
        /// </summary>
        public string Ticker { get; set; }
        /// <summary>
        /// 通知标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 通知文字描述 
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public string AttachInfo { get; set; }
        /// <summary>
        /// 移动设备的Token
        /// </summary>
        public string DeviceToken { get; set; }
        /// <summary>
        /// 移动设备的系统类型
        /// </summary>
        public SystemTypeEnum SystemType { get; set; }

    }
}
