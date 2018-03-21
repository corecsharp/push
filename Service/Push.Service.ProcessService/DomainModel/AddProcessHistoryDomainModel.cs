using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.ProcessService.DomainModel
{
    public class AddProcessHistoryDomainModel
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
        /// 来自哪个App(配置在dic中)
        /// </summary>
        public int AppId { get; set; }
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
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }
        /// <summary>
        /// 发送标志：0：失败， 1:成功
        /// </summary>
        public int SendStatus { get; set; }
        /// <summary>
        ///成功， 返回的唯一标识
        /// </summary>
        public string ReturnSign { get; set; }
        /// <summary>
        ///失败状态：1：失败次数超过3次，2：超时失败 ，3：账号登出 4：配置数据错误 5：发送到推送平台失败
        /// </summary>
        public int? ErrorType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 推迟次数(0),推迟次数越大，越早发送,最多重试3次，3次以后直接失败
        /// </summary>
        public int DelayTimes { get; set; }
        /// <summary>
        /// 品牌Id
        /// </summary>
        public int? BrandId { get; set; }
        /// <summary>
        /// 通道Id
        /// </summary>
        public long? ChannelId { get; set; }
        /// <summary>
        /// 请求的毫秒数
        /// </summary>
        public int RequestTime { get; set; }
        /// <summary>
        /// 记录批次号
        /// </summary>
        public Guid? BatchNo { get; set; }

        /// <summary>
        /// 消息推送服务对设备的唯一标识
        /// </summary>
        public string DeviceToken { get; set; }
    }
}
