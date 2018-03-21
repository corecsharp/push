using Push.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Huawei
{
    internal class MultiCast : HuaweiNotification
    {
        public static SenderRet SendMessage(PushChannelModel channel, List<PushMsgModel> msgList)
        {
            List<SenderRet> senderRetList = new List<SenderRet>();
            foreach (var msg in msgList)
            {
                SenderRet ret = UniCast.SendMessage(channel, msg);
                ret.Id = msg.Id;
                senderRetList.Add(ret);
            }
            return new SenderRet { IsSuccess = true, ResultList = senderRetList };
        }
    }
}
