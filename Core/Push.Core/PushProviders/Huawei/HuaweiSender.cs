using Push.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Huawei
{
    public class HuaweiSender : ISender
    {
        public SenderRet Send(PushChannelModel channel, PushMsgModel msg)
        {
            if (channel.SystemType == SystemTypeEnum.Android || msg.SystemType == SystemTypeEnum.Android)
            {
                return UniCast.SendMessage(channel, msg);
            }
            return new SenderRet() { IsSuccess = false, Code = "-1", Msg = "华为推送通道只支持android" };
        }


        public SenderRet SendList(PushChannelModel channel, List<PushMsgModel> msgList)
        {
            if (channel.SystemType == SystemTypeEnum.Android)
            {
                return MultiCast.SendMessage(channel, msgList);
            }
            return new SenderRet() { IsSuccess = false, Code = "-1", Msg = "华为推送通道只支持android" };
        }
    }
}
