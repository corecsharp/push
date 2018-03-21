using Push.Core.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Mi
{
    public abstract class MiPushNotification : INotifyApplicationService
    {
        private static HttpClient httpClient;
        static MiPushNotification()
        {
            httpClient = new HttpClient();
        }
        public SenderRet SendToMiPush(string bodyStr, string pushUrl, string appMasterSecret, int timeOut = 2)
        {
            SenderRet senderRet = new SenderRet();
            string postBody = bodyStr;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pushUrl);
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8;";
            request.Method = "POST";
            request.Headers[HttpRequestHeader.Authorization] = "key=" + appMasterSecret;
            request.ContinueTimeout = timeOut*1000;
            byte[] bytes = Encoding.UTF8.GetBytes(postBody);//这里编码设置为utf8
            try
            {
                using (Stream os = request.GetRequestStreamAsync().Result)
                {
                    os.Write(bytes, 0, bytes.Length);
                }
                WebResponse resp = request.GetResponseAsync().Result;
                if (resp == null)
                {
                    return new SenderRet() { IsSuccess = false, Msg = "返回为空" };
                }
                StreamReader sr = new StreamReader(resp.GetResponseStream());
                string ret = sr.ReadToEnd().Trim();
                MiRetModel retModel = JsonConvert.DeserializeObject<MiRetModel>(ret);
                senderRet.IsSuccess = retModel.code == "0";
                if (senderRet.IsSuccess)
                {
                    senderRet.Sign = retModel.data.id;//发送成功时返回的标识
                    senderRet.Msg = retModel.description;
                }
                else
                {
                    senderRet.Code = string.Format("Code:{0}", retModel.code);
                    senderRet.Msg = retModel.description;
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse res = (HttpWebResponse)ex.Response;
                senderRet.IsSuccess = false;
                senderRet.Msg = ex.Message;
                if (res != null)
                {
                    senderRet.Code = string.Format("Code:{0}", res.StatusCode);
                }
            }
            return senderRet;
        }


        /// <summary>
        /// 推送消息
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public virtual SenderRet SendMessage(object channel, object msg)
        {
            return new SenderRet();
        }
    }
}
