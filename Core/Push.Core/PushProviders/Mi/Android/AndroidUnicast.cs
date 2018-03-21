using Push.Core.Infrastructure;
using Push.Core.Infrastructure.Util;
using Sherlock;
using Sherlock.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Mi.Android
{
    public class AndroidUnicast : AndroidNotification
    {
        public AndroidUnicast()
        {
        }

        public string InitMiMessage(PushMsgModel msg)
        {
            StringBuilder body = newBody("registration_id", SherlockWebUtility.UrlEncode(msg.DeviceToken));

            //推送Msg
            if (!string.IsNullOrEmpty(msg.Msg))
            {
                addParameter(body, "payload", SherlockWebUtility.UrlEncode(msg.Msg));
                addParameter(body, "description", SherlockWebUtility.UrlEncode(msg.Msg));
            }
            //推送标题
            if (!string.IsNullOrEmpty(msg.Title))
            {
                addParameter(body, "title", SherlockWebUtility.UrlEncode(msg.Title));
            }
            addParameter(body, "notify_type", "1");//提醒类型
            if (!string.IsNullOrEmpty(msg.AttachInfo))
            {
                Dictionary<string, string> extra = DictionaryUtil<string, string>.StringToDictionary(msg.AttachInfo);
                if (extra != null)
                {
                    foreach (var item in extra)
                    {
                        addParameter(body, string.Format("extra.{0}", item.Key), SherlockWebUtility.UrlEncode(item.Value));
                    }
                }

            }
            string bodyStr = body.ToString();
            if (!string.IsNullOrEmpty(bodyStr) && bodyStr.ElementAt(0) == '&')
                bodyStr = bodyStr.Substring(1);
            return body.ToString();
        }


        /// <summary>
        /// 单播发送友盟消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override SenderRet SendMessage(object channel, object msg)
        {
            PushChannelModel pushChannelModel = channel as PushChannelModel;
            PushMsgModel pushMsgModel = msg as PushMsgModel;
            string url = pushChannelModel.Url;
            string appSecret = pushChannelModel.AppSecret;
            var bodyStr = InitMiMessage(pushMsgModel);
            var ret = SendToMiPush(bodyStr, url, appSecret);
            return ret;
        }

    }
}
