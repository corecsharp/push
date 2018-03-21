using Push.Api.Logic.Common;
using Push.Core.PushProviders.Huawei;
using Push.Core.PushProviders.Mi;
using Push.Core.PushProviders.UM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.Config
{
    /// <summary>
    /// 推送信息供应商配置
    /// </summary>
    public class PushSenderConfig
    {
        /// <summary>
        /// 初始化推送信息供应商
        /// </summary>
        public static void InitSender()
        {
            PushSenderManager.AddOrUpdateSender(1, new UMSender());//友盟推送
            //PushSenderManager.AddOrUpdateSender(2, new AppleSender());//苹果推送（废弃）
            PushSenderManager.AddOrUpdateSender(3, new MiSender());//小米推送
            PushSenderManager.AddOrUpdateSender(4, new HuaweiSender());//华为推送
        }
    }
}
