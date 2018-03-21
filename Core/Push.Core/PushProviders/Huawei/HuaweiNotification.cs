using Platform.Core.Helper;
using Push.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Push.Core.PushProviders.Huawei
{
    internal class HuaweiNotification
    {
        static AccessToken _accessToken = new AccessToken();
        static string _lock = string.Empty;

        /// <summary>
        /// 发送推送
        /// </summary>
        /// <param name="url"></param>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SenderRet SendToHuaweiPush(string url, string appKey, string appSecret, int timeout, PushMsgModel model)
        {
            try
            {
                string accessToken = GetAccessToken(appKey, appSecret);
                string postData = GeneratePostBody(model, accessToken);

                HttpWebRequest request = HttpWebRequest.CreateHttp(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "gzip";
                request.ContinueTimeout = timeout;
                using (StreamWriter sendStream = new StreamWriter(request.GetRequestStreamAsync().Result))
                {

                    sendStream.Write(postData);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("gbk"));
                    var msg = sr.ReadToEnd();
                    PushRetModel ret = Newtonsoft.Json.JsonConvert.DeserializeObject<PushRetModel>(msg);
                    if (ret.resultcode != null)
                    {
                        string code = null;
                        if (ret.resultcode != "0")
                            code = "Code:" + ret.resultcode;
                        return new SenderRet()
                        {
                            Code = code,
                            IsSuccess = ret.resultcode == "0" ? true : false,
                            Msg = ret.message,
                            Sign = ret.requestID
                        };
                    }
                    return new SenderRet()
                    {
                        IsSuccess = false,
                        Msg = ret.message
                    };
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error.Write("HuaweiNotification", ex);
                return new SenderRet()
                {
                    IsSuccess = false,
                    Msg = ex.Message
                };
            }
        }
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static string GetAccessToken(string appKey, string appSecret)
        {
            lock (_lock)
            {
                if (_accessToken != null)
                    _accessToken = new AccessToken();
                if (!string.IsNullOrWhiteSpace(_accessToken.Token) && _accessToken.UpdateTime >= DateTimeHelper.GetNow().AddDays(-6))
                    return _accessToken.Token;

                HttpWebRequest client = HttpWebRequest.CreateHttp("https://login.vmall.com/oauth2/token");
                client.Method = "POST";
                client.ContentType = "application/x-www-form-urlencoded";
                client.Accept = "gzip";

                string postData = "grant_type=client_credentials&client_id=" + appKey + "&client_secret=" + appSecret;
                byte[] bytes = Encoding.ASCII.GetBytes(postData);
                using (Stream sendStream = client.GetRequestStreamAsync().Result) {
                    sendStream.Write(bytes, 0, bytes.Length);
                }
                HttpWebResponse response = (HttpWebResponse)client.GetResponseAsync().Result;
                using (Stream responseStream = response.GetResponseStream())
                {

                    StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("gbk"));
                    var msg = sr.ReadToEnd();
                    var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<GetAccessTokenRetModel>(msg);
                    _accessToken.Token = ret.Access_Token;
                    _accessToken.UpdateTime = DateTimeHelper.GetNow();
                }
                return _accessToken.Token;
            }
        }
        /// <summary>
        /// 生成消息体
        /// </summary>
        /// <param name="model"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static string GeneratePostBody(PushMsgModel model, string accessToken)
        {
            List<Dictionary<string, object>> extra = new List<Dictionary<string, object>>();
            if (model.AttachInfo != null)
                extra = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(model.AttachInfo).ConvertToDicList();
            AndroidModel androidModel = new AndroidModel(model.Title, model.Msg, 1, extra);
            string android = Newtonsoft.Json.JsonConvert.SerializeObject(androidModel);
            int cacheMode = 1;
            int msgType = 1;
            long nsp_ts = (long)(DateTimeHelper.GetNow().Subtract(new DateTime(1970, 1, 1)).TotalSeconds);

            // 构造请求
            Dictionary<string, object> dic = new Dictionary<string, object>();

            //必选 ，取值建议固定为“JSON”，另外两种取值为：“php-rpc”和“JS”
            dic.Add("nsp_fmt", "JSON");
            // 目标用户，必选。
            // 由客户端获取， 32 字节长度。手机上安装了push应用后，会到push服务器申请token，申请到的token会上报给应用服务器
            //dic.Add("deviceToken", HttpUtility.UrlEncode(model.DeviceToken));
            //// 必选 使用 OAuth2进行鉴权时的 AccessToken
            //dic.Add("access_token", HttpUtility.UrlEncode(accessToken));
            //// 可选
            //// 0: 当前用户
            //// 1: 主要用户
            //// -1: 默认用户
            //dic.Add("userType", 1);
            //// 发送到设备上的消息，必选
            //// 最长为4096 字节（开发者自定义，自解析）
            //dic.Add("android", HttpUtility.UrlEncode(android));
            // 消息是否需要缓存，必选
            // 0：不缓存
            // 1：缓存
            // 缺省值为0
            dic.Add("cacheMode", cacheMode);
            // 标识消息类型（缓存机制），必选
            // 由调用端赋值，取值范围（1~100）。当TMID+msgType的值一样时，仅缓存最新的一条消息
            dic.Add("msgType", msgType);
            // unix时间戳，可选
            // 格式：2013-08-29 19:55
            // 消息过期删除时间
            // 如果不填写，默认超时时间为当前时间后48小时
            dic.Add("nsp_ts", nsp_ts);
            // 单播模式
            dic.Add("nsp_svc", "openpush.message.psSingleSend");

            string postData = dic.ConvertToParam();

            return postData;
        }
    }

    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime UpdateTime { get; set; }

        public AccessToken()
        {
            Token = null;
            UpdateTime = DateTime.MinValue;
        }
    }
}
