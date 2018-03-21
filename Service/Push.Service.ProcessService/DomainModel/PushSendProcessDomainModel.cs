using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Push.Service.ProcessService.DomainModel
{
    /// <summary>
    /// 待推送消息
    /// </summary>
    public class PushSendProcessDomainModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 推送消息的序列号
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// App编号  (1000:B2C国资文旅 ,1001: B2B国资云农 ,1002:B2C国资商城 ,1003:B2B国资妙鲜) 
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// 第一次发送失败，重新插入一条并赋值
        /// </summary>
        public long? TokenBrandId { get; set; }

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
        /// 优先级 1：紧急型 2：中优先级 3：低优先级（默认给2）
        /// </summary>
        public int PriorityLevel { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间（默认24小时）
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否被使用，数据处理使用
        /// </summary>
        public bool IsUsed { get; set; }

        /// <summary>
        /// 过期时间，数据处理使用
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 推迟发送时间，初始值为StartTime
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 推迟次数(0),推迟次数越大，越早发送,最多重试3次，3次以后直接失败
        /// </summary>
        public int DelayTimes { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public virtual Guid? BatchNo { get; set; }

        /// <summary>
        /// 品牌Id
        /// </summary>
        public virtual int? BrandId { get; set; }

        /// <summary>
        /// 通道Id
        /// </summary>
        public virtual long? ChannelId { get; set; }

        /// <summary>
        /// 消息推送服务对设备的唯一标识（第三方标识）
        /// </summary>
        public virtual string DeviceToken { get; set; }


        public string GetSerialNO()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
