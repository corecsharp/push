using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.Infrastructure
{
    /// <summary>
    /// 推送的通道
    /// </summary>
    public class PushChannelModel
    {
        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }
        /// <summary>
        /// 通道路劲
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 推送应用的唯一标识
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 推送应用的秘钥(小米必传)
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 正式/测试模式  配置在Config表中
        /// </summary>
        public bool ProductionMode { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// App的证书路劲
        /// </summary>
        public string AppCertParth { get; set; }

        /// <summary>
        /// App的证书密码
        /// </summary>
        public string AppCertPassword { get; set; }

        public SystemTypeEnum SystemType { get; set; }
        /// <summary>
        /// 请求第三方超时时间（秒）
        /// </summary>
        public int TimeOut { get; set; }
        /// <summary>
        /// 一次批量推送的条数
        /// </summary>
        public int PushNum { get; set; }
    }
}
