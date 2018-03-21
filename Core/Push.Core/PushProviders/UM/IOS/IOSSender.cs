using Push.Core.DTOs.UM;
using Push.Core.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.UM.IOS
{
    public class IOSSender
    {
        private static readonly System.DateTime Jan1st1970 = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

        public static long CurrentUnixTimeMillis()
        {
            return (long)(System.DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        private static PushApplicationSession pushApplicationSession;

        static IOSSender()
        {
            pushApplicationSession = new PushApplicationSession();
        }

        public static SenderRet PushMsg(PushChannelModel channel, PushMsgModel msg)
        {
            IOSPushDto iospush = PushMsgModelToIOSPushDto(channel, msg);
            int timeOut = channel.PushNum == 0 ? 2 : (int)Math.Ceiling((double)channel.TimeOut / channel.PushNum);
            return pushApplicationSession.IPushIOSUnicast.SendMessage(iospush, timeOut);
        }

        public static IOSPushDto PushMsgModelToIOSPushDto(PushChannelModel channel, PushMsgModel msg)
        {
            IOSPushDto iospush = new IOSPushDto
            {
                PushUrl = channel.Url,
                AppKey = channel.AppKey,
                AppMasterSecret = channel.AppSecret,
                DeviceTokens = msg.DeviceToken,
                ProductionMode = channel.ProductionMode,
                Alert = msg.Msg,
                Description = msg.Title,
                TimeStamp = Convert.ToString((int)(CurrentUnixTimeMillis() / 1000)),
                Sound = "default"
            };
            if (!string.IsNullOrEmpty(msg.AttachInfo))
            {
                try
                {
                    iospush.Extra = JsonConvert.DeserializeObject<Dictionary<string, string>>(msg.AttachInfo);
                }
                catch
                {
                     //LogHelper.Info.Write("PushMsg", string.Format("AttachInfo:{0},字段中内容不符合规范，序列化失败，置为空继续发送", msg.AttachInfo));
                    iospush.Extra = null;
                }
            }
            return iospush;
        }


        public static SenderRet PushMsgList(PushChannelModel channel, List<PushMsgModel> msgList)
        {

            List<SenderRet> senderRetList = new List<SenderRet>();
            foreach (var item in msgList)
            {
                SenderRet ret = PushMsg(channel, item);
                ret.Id = item.Id;
                senderRetList.Add(ret);
            }
            return new SenderRet { IsSuccess = true, ResultList = senderRetList };
        }


    }
}
