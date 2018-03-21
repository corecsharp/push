using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.MessageCenterService.DBModel
{
    /// <summary>
    /// 推送消息
    /// </summary>
    public class PushMessage
    {  
       /// <summary>
       /// 主键id
       /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// app主键id
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 附加信息
        /// </summary>
        public string AttachInfo { get; set; }

        /// <summary>
        /// 阅读状态：0未读 1：已读 ，99消息被清空
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 消息类别：用户自定义
        /// </summary>
        public int MessageType { get; set; }

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
