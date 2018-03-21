using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.DTOs.Mi
{
    /// <summary>
    /// 小米推送的AndroidPushDto
    /// </summary>
    public class AndroidPushDto
    {
        /// <summary>
        /// 必填 供API对接推送服务器使用
        /// </summary>
        public string AppMasterSecret { get; set; }

        /// <summary>
        /// 必填 应用唯一标识
        /// </summary>
        public string AppKey { get; set; }

        public string CollapseKey { get; set; }
        public string PackageName { get; set; }
        public long? TimeToLive { get; set; }
        public string DeviceToken { get; set; }
        public string Alias { get; set; }
        /// <summary>
        /// 必填 通知栏提示文字
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 必填 通知标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 必填 通知文字描述
        /// </summary>
        public string PayLoad { get; set; }
        public int? PassThrough { get; set; }
        public int? NotifyId { get; set; }
        public int? TimeToSend { get; set; }
        public string Url { get; set; }
        public Dictionary<string, string> Extra { get; set; }
        public bool ProductionMode { get; set; }

        /// <summary>
        /// 可选 发送消息描述，建议填写。
        /// </summary>
        public string Discription { get; set; }



    }
}
