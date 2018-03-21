using Push.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Huawei
{
    internal class UniCast : HuaweiNotification
    {
        public static SenderRet SendMessage(PushChannelModel channel, PushMsgModel msg)
        {
            int timeout = channel.TimeOut * 1000 / channel.PushNum;
            return SendToHuaweiPush(channel.Url, channel.AppKey, channel.AppSecret, timeout, msg);
        }
    }
}
