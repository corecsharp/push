using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Huawei
{
    public class AndroidModel
    {
        /// <summary>
        /// （必填）
        /// Notification bar上显示的标题
        /// </summary>
        public string notification_title { get; set; }

        /// <summary>
        /// （必填）
        /// Notification bar上显示的内容
        /// </summary>
        public string notification_content { get; set; }

        /// <summary>
        /// （必填）
        /// 1：直接打开应用
        /// 2：通过自定义动作打开应用
        /// 3：打开URL
        /// 4：富媒体消息
        /// 5：短信收件箱广告
        /// 6：彩信收件箱广告
        /// 注意：当手机收到短信、彩信收件箱广告后，在收件人一栏显示的是应用在联盟上注册的名字哦 ~
        /// </summary>
        public int doings { get; set; }

        /// <summary>
        /// 系统小图标名称
        /// 该图标预置在客户端，在通知栏顶部展示
        /// </summary>
        //public string Notification_Status_Icon { get; set; }
        /// <summary>
        /// 仅富媒体消息需要填写该字段
        /// 富媒体消息的附件为.zip格式，客户端解压后默认打开index.html。".zip"文件的大小不能超过100K哦，否则会拒绝发送的
        /// 只要在开发者联盟上实名认证，都会赠送5G的云存储空间，可以在开发者联盟portal给每个应用申请云存储空间。富媒体附件一定要保存到云存储空间的哦 ~~
        /// 云存储文件地址格式：http://cs.dbank.com/dl/appName/path/filename，其中"http://cs.dbank.com/dl/"为写死的部分，"appName/path/file"应用可以根据自身实际情况填写
        /// 如何上传附件请参考：资料中心->云存储服务->SDK下载 中的样例代码
        /// </summary>
        //public string Content_File_Url { get; set; }
        /// <summary>
        /// 短信收件箱广告内容
        /// 当doings的取值为5时，该字段必须填写
        /// </summary>
        //public string SMSContent { get; set; }
        /// <summary>
        /// 彩信收件箱广告附件链接
        /// 当doings的取值为6时，该字段必须填写
        /// 彩信附件为.zip格式，压缩包中必须要有.smil描述文件，并且符合标准smil语法哦。.zip文件的大小不能超过100K哦，否则会拒绝发送的
        /// 只要在开发者联盟上实名认证，都会赠送5G的云存储空间，可以在开发者联盟portal给每个应用申请云存储空间。富媒体附件一定要保存到云存储空间的哦~~
        /// 云存储文件地址格式：http://cs.dbank.com/dl/appName/path/filename，其中“http://cs.dbank.com/dl/”为写死的部分，“appName/path/file” 应用可以根据自身实际情况填写
        /// 如何上传附件请参考：资料中心->云存储服务->SDK下载 中的样例代码
        /// </summary>
        //public string MMSUrl { get; set; }
        /// <summary>
        /// 链接
        /// 当doings的取值为3时，必须携带该字段
        /// </summary>
        //public string Url { get; set; }
        /// <summary>
        /// 自定义打开应用动作
        /// 当doings的取值为2时，必须携带该字段
        /// </summary>
        //public string Intent { get; set; }
        /// <summary>
        /// 用户自定义键值对
        /// "extras":[{"season":"Spring"},{"weather":"raining"}]
        /// </summary>
        public List<Dictionary<string, object>> extras { get; set; }

        public AndroidModel(string Notification_Title, string Notification_Content, int Doings, List<Dictionary<string, object>> Extras)
        {
            this.notification_title = Notification_Title;
            this.notification_content = Notification_Content;
            this.doings = Doings;
            this.extras = Extras;
        }
    }
}
