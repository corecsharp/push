using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.Infrastructure
{
    public interface ISender
    {
        SenderRet Send(PushChannelModel channel, PushMsgModel msg);

        SenderRet SendList(PushChannelModel channel, List<PushMsgModel> msgList);
    }
}
