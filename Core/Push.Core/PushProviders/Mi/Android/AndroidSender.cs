using Push.Core.DTOs.Mi;
using Push.Core.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Mi.Android
{
    public class AndroidSender
    {

        private static PushApplicationSession pushApplicationSession;

        static AndroidSender()
        {
            pushApplicationSession = new PushApplicationSession();
        }

        public static SenderRet PushMsg(PushChannelModel channel, PushMsgModel msg)
        {
            return pushApplicationSession.IPushAndroidUnicast.SendMessage(channel, msg);
        }


        public static AndroidPushDto PushMsgModelToAndroidPushDto(PushChannelModel channel, PushMsgModel msg)
        {
            AndroidPushDto androidpush = new AndroidPushDto
            {
                DeviceToken = msg.DeviceToken,
                ProductionMode = channel.ProductionMode,
                Description = msg.Ticker,
                Title = msg.Title,
                PayLoad = msg.Msg
            };
            if (!string.IsNullOrEmpty(msg.AttachInfo))
            {
                try
                {
                    androidpush.Extra = JsonConvert.DeserializeObject<Dictionary<string, string>>(msg.AttachInfo);
                }
                catch
                {
                    //LogHelper.Info.Write("PushMsg", string.Format("AttachInfo:{0},字段中内容不符合规范，序列化失败，置为空继续发送", msg.AttachInfo));
                    androidpush.Extra = null;
                }
            }
            return androidpush;
        }


        public static SenderRet PushMsgList(PushChannelModel channel, List<PushMsgModel> msgList)
        {
            return pushApplicationSession.IPushAndroidMulticast.SendMessage(channel, msgList);
        }


    }
}
