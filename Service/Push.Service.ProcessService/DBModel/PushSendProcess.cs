using System;

namespace Push.Service.ProcessService.DBModel
{
    /// <summary>
    /// 待推送的消息，如果一个token对应多个手机，则一条消息拆成多条去发，如果发送失败，回写待发送消息，记录哪个手机设备发送失败。
    /// </summary>
    public class PushSendProcess
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// 手机注册的token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// app主键id
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// 第一次发送失败，重新插入一条并赋值
        /// </summary>
        public long? TokenBrandId { get; set; }

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
        /// 优先级
        /// </summary>
        public int PriorityLevel { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 延迟时间
        /// </summary>
        public int DelayTimes { get; set; }

        /// <summary>
        /// 批量号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 批量失效时间
        /// </summary>
        public DateTime? BatchExpireTime { get; set; }

        /// <summary>
        /// 品牌id
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// 通道id
        /// </summary>
        public long ChannelId { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string DeviceToken { get; set; }

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
