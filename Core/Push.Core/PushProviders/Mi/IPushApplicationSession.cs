using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Mi
{
    /// <summary>
    /// 推送仓储接口
    /// </summary>
    public interface IPushApplicationSession
    {
        /// <summary>
        /// Android 单播
        /// </summary>
        INotifyApplicationService IPushAndroidUnicast { get; }
    }
}
