using System;

namespace Push.Service.ProcessService.DBModel
{
    /// <summary>
    /// 推送消息历史发送记录。记录，无论成功或失败。
    /// </summary>
    public class PushProcessHistory
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
        /// 手机注册Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// app主键id
        /// </summary>
        public long AppId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息
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
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        public int SendStatus { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string ReturnSign { get; set; }

        /// <summary>
        /// 错误类别，如果有错误。
        /// </summary>
        public int? ErrorType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 延迟时间
        /// </summary>
        public int DelayTimes { get; set; }

        /// <summary>
        /// 批量号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 品牌id
        /// </summary>
        public long BrandId { get; set; }

        /// <summary>
        /// 通道id
        /// </summary>
        public long ChannelId { get; set; }

        /// <summary>
        /// 设备令牌
        /// </summary>
        public string DeviceToken { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public int RequestTime { get; set; }

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
