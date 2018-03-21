using System;
using System.Collections.Generic;

namespace Push.Api.DTOs
{
    public class SendMsgRequest : SendMsgDto
    {
        /// <summary>
        /// UserId
        /// </summary>
        public string Token { get; set; }

    }


    public class SendMsgDto
    {
        /// <summary>
        /// App编号。 1000 国资文旅，1001 国资云农，1002 国资商城
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// 推送内容
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 附加参数
        /// </summary>
        public Dictionary<string, string> AttachInfo { get; set; }
        /// <summary>
        /// 通知标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息类型：用户自定义
        /// </summary>
        public int MsgType { get; set; }                                            

        /// <summary>
        /// 发送时间，选填，不填则立刻发送
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 时效时间，选填，不填则默认24小时，单位为分钟，这条消息如果超过发送时间加上失效时间未发送的话则不再发送
        /// </summary>
        public int? Timeliness { get; set; }
    }
}
