using Push.Core.Infrastructure;
using Push.Core.PushProviders.UM.Android;
using Push.Core.PushProviders.UM.IOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.UM
{
    public class UMSender : ISender
    {
        public SenderRet Send(PushChannelModel channel, PushMsgModel msg)
        {

            if (msg.SystemType == SystemTypeEnum.Android)
            {
                return AndroidSender.PushMsg(channel, msg);
            }
            return IOSSender.PushMsg(channel, msg);

        }


        public SenderRet SendList(PushChannelModel channel, List<PushMsgModel> msgList)
        {

            if (channel.SystemType == SystemTypeEnum.Android)
            {
                return AndroidSender.PushMsgList(channel, msgList);
            }
            return IOSSender.PushMsgList(channel, msgList);

        }
    }
}
