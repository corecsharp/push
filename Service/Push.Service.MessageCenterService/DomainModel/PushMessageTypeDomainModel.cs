using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.MessageCenterService.DomainModel
{
    /// <summary>
    /// 消息中心
    /// </summary>
    public class PushMessageTypeDomainModel
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public int MessageType { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message { get; set; }
    }
}
