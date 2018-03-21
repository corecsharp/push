using Push.Core.Infrastructure;
using Push.Core.PushProviders.Mi.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Mi
{
    public class MiSender : ISender
    {
        public SenderRet Send(PushChannelModel channel, PushMsgModel msg)
        {

            //目前MiPush只做小米手机（Android系统）
            return AndroidSender.PushMsg(channel, msg);
        }

        public SenderRet SendList(PushChannelModel channel, List<PushMsgModel> msgList)
        {

            //目前MiPush只做小米手机（Android系统）
            //TODO 批量推送
            return AndroidSender.PushMsgList(channel, msgList);
        }
    }
}
