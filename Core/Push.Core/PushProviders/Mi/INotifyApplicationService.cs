using Push.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Mi
{
    public interface INotifyApplicationService
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        SenderRet SendMessage(object channel, object msg);
    }
}
