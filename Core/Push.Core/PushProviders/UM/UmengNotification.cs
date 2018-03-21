using Push.Core.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.UM
{
    public abstract class UmengNotification : INotifyApplicationService
    {
        private static HttpClient httpClient;
        private static object lockObject = new object();

        protected internal static readonly HashSet<string> ROOT_KEYS = new HashSet<string>((new string[] { "appkey", "timestamp", "type", "device_tokens", "alias", "alias_type", "file_id", "filter", "production_mode", "feedback", "description", "thirdparty_id" }).ToList());

        protected internal static readonly HashSet<string> POLICY_KEYS = new HashSet<string>((new string[] { "start_time", "expire_time", "max_send_num" }).ToList());

        public abstract bool setPredefinedKeyValue(JObject rootJson, string key, string value);

        public virtual string InitPushUrl(object obj)
        {
            return string.Empty;
        }

        public virtual string InitAppMasterSecret(object obj)
        {
            return string.Empty;
        }

        static UmengNotification()
        {
            httpClient = new HttpClient();
        }

        public SenderRet SendToUM(JObject rootJson, string pushUrl, string appMasterSecret, int timeOut = 2)
        {
            SenderRet senderRet = new SenderRet { IsSuccess = true };
            string postBody = rootJson.ToString();
            string sign = GetMd5Hex("POST" + pushUrl + postBody + appMasterSecret);
            pushUrl = pushUrl + "?sign=" + sign;
            HttpContent httpContent = new StringContent(postBody);
            httpContent.Headers.Add("Timeout", (timeOut*1000).ToString());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, pushUrl);
            httpRequestMessage.Content = httpContent;
            try
            {
                var response = httpClient.SendAsync(httpRequestMessage).Result;
                var responseStr = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    UMRetModel retModel = JsonConvert.DeserializeObject<UMRetModel>(responseStr);
                    senderRet.Sign = retModel.data.msg_id;//发送成功时返回的标识
                    senderRet.Msg = responseStr;
                }
                else
                {
                    senderRet.IsSuccess = false;
                    UMRetModel retModel = JsonConvert.DeserializeObject<UMRetModel>(responseStr);
                    senderRet.Msg = responseStr;
                    senderRet.Code = string.Format("Code:{0}", retModel.data.error_code);
                    if (retModel.data.error_code == "2004")
                    {
                        //返回需要配置的Ip
                        senderRet.Code += string.Format(",Ip:{0}", retModel.data.ip);
                    }
                }
            }
            catch (Exception ex)
            {
                senderRet.IsSuccess = false;
                senderRet.Msg = ex.Message;
            }
            return senderRet;
        }

        protected string GetMd5Hex(string paramsString)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(paramsString));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bytes)
                    sb.AppendFormat("{0:X2}", b);
                return sb.ToString().ToLower();
            }
        }

        /// <summary>
        ///  初始化友盟消息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual JObject InitUmMessage(object obj)
        {
            return null;
        }

        /// <summary>
        /// 发送友盟消息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public SenderRet SendMessage(object obj, int timeOut)
        {
            JObject jsonObject = InitUmMessage(obj);
            string secret = InitAppMasterSecret(obj);
            string url = InitPushUrl(obj);
            if (jsonObject == null || string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(url)) return new SenderRet();
            return SendToUM(jsonObject, url, secret, timeOut);
        }
    }
}
