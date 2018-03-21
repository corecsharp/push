using System.Collections.Generic;

namespace Push.Core.Infrastructure
{
    public static class PushSender
    {
        public static SenderRet Send(ISender sender, PushChannelModel channel, PushMsgModel msg)
        {
           return  sender.Send(channel,msg);
        }

        public static SenderRet Send(ISender sender, PushChannelModel channel, List<PushMsgModel> msgList)
        {
            return sender.SendList(channel, msgList);
        }
    }
}
