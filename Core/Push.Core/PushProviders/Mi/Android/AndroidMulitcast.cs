using Push.Core.Infrastructure;
using Push.Core.Infrastructure.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Mi.Android
{
    /// <summary>
    /// 多播
    /// </summary>
    public class AndroidMulitcast : AndroidNotification
    {
        public AndroidMulitcast()
        {

        }

        public string InitMiMultiMessage(List<PushMsgModel> msgList)
        {
            if (msgList == null)
            {
                return null;
            }
            StringBuilder body = new StringBuilder();
            body.Append("messages=");
            dynamic messagesList = msgList.Select(e =>
            {
                Dictionary<string, string> extra = DictionaryUtil<string, string>.StringToDictionary(e.AttachInfo);
                return new
                {
                    target = e.DeviceToken,
                    message = new
                    {
                        payload = e.Msg,
                        notify_type = 1,
                        description = e.Msg,
                        title = e.Title,
                        extra = extra
                    },
                };
            }).ToList();
            string messages = JsonConvert.SerializeObject(messagesList);
            body.Append(messages);
            return body.ToString();
        }

        public override SenderRet SendMessage(object channel, object msgList)
        {
            PushChannelModel pushChannelModel = channel as PushChannelModel;
            List<PushMsgModel> pushMsgModelList = msgList as List<PushMsgModel>;
            string url = pushChannelModel.Url;
            string appSecret = pushChannelModel.AppSecret;
            var bodyStr = InitMiMultiMessage(pushMsgModelList);
            int timeOut = pushChannelModel.TimeOut;
            var ret = SendToMiPush(bodyStr, url, appSecret, timeOut);
            return ret;
        }
    }
}
