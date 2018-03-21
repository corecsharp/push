using Push.Core.Enum.UM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.DTOs.UM
{
    public class AndroidPushDto
    {
        /// <summary>
        /// 必填 供API对接友盟服务器使用
        /// </summary>
        public string AppMasterSecret { get; set; }

        /// <summary>
        /// 必填 应用唯一标识
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 只有在要用到FileCast的时候才会用到
        /// 里面存放多个devicetoken，多个token之间显示用\n隔开
        /// 比如"device_token_1\ndevice_token_2\ndevice_token_3\n..."
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 必填 时间戳，10位或者13位均可，时间戳有效期为10分钟
        /// </summary>
        public string TimeStamp { get; set; }
        ///// <summary>
        ///// 必填 消息发送类型
        ///// </summary>
        //public PushType Casttype { get; set; }

        /// <summary>
        /// 可选 设备唯一表示
        /// 当type=unicast时,必填, 表示指定的单个设备
        /// 当type=listcast时,必填,要求不超过500个,
        /// 多个device_token以英文逗号间隔
        /// </summary>
        public string DeviceTokens { get; set; }
        /// <summary>
        /// 可选 当type=customizedcast时，必填，alias的类型, 
        /// alias_type可由开发者自定义,开发者在SDK中
        /// 调用setAlias(alias, alias_type)时所设置的alias_type
        /// </summary>
        public string AliasType { get; set; }
        /// <summary>
        /// 可选 当type=customizedcast时，必填，alias的类型, 
        ///      alias_type可由开发者自定义,开发者在SDK中
        ///      调用setAlias(alias, alias_type)时所设置的alias_type
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 可选 当type=filecast时，file内容为多条device_token, 
        ///        device_token以回车符分隔
        ///      当type=customizedcast时，file内容为多条alias，
        ///        alias以回车符分隔，注意同一个文件内的alias所对应
        ///        的alias_type必须和接口参数alias_type一致。
        ///      注意，使用文件播前需要先调用文件上传接口获取file_id
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// 可选 终端用户筛选条件,如用户标签、地域、应用版本以及渠道等
        /// </summary>
        public dynamic Filter { get; set; }

        /// <summary>
        /// 必填 消息类型，值可以为:
        ///      notification-通知，message-消息
        /// </summary>
        public DisplayType Displaytype { get; set; }

        /// <summary>
        /// 必填 通知栏提示文字
        /// </summary>
        public string Ticker { get; set; }
        /// <summary>
        /// 必填 通知标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 必填 通知文字描述
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 可选 状态栏图标ID, R.drawable.[smallIcon],
        ///      如果没有, 默认使用应用图标。
        ///      图片要求为24*24dp的图标,或24*24px放在drawable-mdpi下。
        ///      注意四周各留1个dp的空白像素
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 可选 通知栏拉开后左侧图标ID, R.drawable.[largeIcon].
        ///      图片要求为64*64dp的图标,
        ///      可设计一张64*64px放在drawable-mdpi下,
        ///      注意图片四周留空，不至于显示太拥挤
        /// </summary>
        public string LargeIcon { get; set; }
        /// <summary>
        /// 可选 通知栏大图标的URL链接。该字段的优先级大于largeIcon。
        ///      该字段要求以http或者https开头。
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// 可选 通知声音，R.raw.[sound]. 
        ///      如果该字段为空，采用SDK默认的声音, 即res/raw/下的
        ///          umeng_push_notification_default_sound声音文件
        ///      如果SDK默认声音文件不存在，
        ///          则使用系统默认的Notification提示音。
        /// </summary>
        public string Sound { get; set; }
        /// <summary>
        /// 可选 默认为0，用于标识该通知采用的样式。使用该参数时,
        ///   开发者必须在SDK里面实现自定义通知栏样式。
        /// </summary>
        public int BuildId { get; set; }

        /// <summary>
        /// 可选 收到通知是否震动,默认为"true".
        /// 注意，"true/false"为字符串
        /// </summary>
        public bool PlayVibrate { get; set; }
        /// <summary>
        /// 可选 收到通知是否闪灯,默认为"true"
        /// </summary>
        public bool PlayLights { get; set; }
        /// <summary>
        /// 可选 收到通知是否发出声音,默认为"true"
        /// </summary>
        public bool PlaySound { get; set; }
        /// <summary>
        /// 点击"通知"的后续行为，默认为打开app。
        ///  必填 值可以为:
        ///     "go_app": 打开应用
        ///     "go_url": 跳转到URL
        ///     "go_activity": 打开特定的activity
        ///     "go_custom": 用户自定义内容。
        /// </summary>
        public AfterOpenAction AfterOpen { get; set; }
        /// <summary>
        /// 可选 当"after_open"为"go_url"时，必填。
        ///      通知栏点击后跳转的URL，要求以http或者https开头
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 可选 当"after_open"为"go_activity"时，必填。
        ///      通知栏点击后打开的Activity
        /// </summary>
        public string Activity { get; set; }
        /// <summary>
        /// 可选 display_type=message, 或者
        ///      display_type=notification且
        ///      "after_open"为"go_custom"时，
        ///      该字段必填。用户自定义内容, 可以为字符串或者JSON格式。
        /// </summary>
        public string Custom { get; set; }

        /// <summary>
        /// 可选 用户自定义key-value。只对"通知"
        ///      (display_type=notification)生效。
        ///      可以配合通知到达后,打开App,打开URL,打开Activity使用。
        /// </summary>
        public Dictionary<string, string> Extra { get; set; }
        /// <summary>
        /// 可选 定时发送时间，若不填写表示立即发送。
        ///      定时发送时间不能小于当前时间
        ///      格式: "YYYY-MM-DD HH:mm:ss"。 
        ///      注意, start_time只对任务生效。
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 可选 消息过期时间,其值不可小于发送时间或者
        ///      start_time(如果填写了的话), 
        ///      如果不填写此参数，默认为3天后过期。格式同start_time
        /// </summary>
        public string ExpireTime { get; set; }
        /// <summary>
        /// 可选 发送限速，每秒发送的最大条数。
        ///      开发者发送的消息如果有请求自己服务器的资源，可以考虑此参数。
        /// </summary>
        public int MaxSendNum { get; set; }
        /// <summary>
        /// 可选 开发者对消息的唯一标识，服务器会根据这个标识避免重复发送。
        ///    有些情况下（例如网络异常）开发者可能会重复调用API导致
        ///    消息多次下发到客户端。如果需要处理这种情况，可以考虑此参数。
        ///    注意, out_biz_no只对任务生效。
        /// </summary>
        public string OutBizNo { get; set; }
        /// <summary>
        /// 可选 正式/测试模式。测试模式下，广播/组播只会将消息发给测试设备。
        ///      测试设备需要到web上添加。
        ///      Android: 测试设备属于正式设备的一个子集。
        /// </summary>
        public bool ProductionMode { get; set; }

        /// <summary>
        /// 可选 发送消息描述，建议填写。
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 可选 开发者自定义消息标识ID, 开发者可以为同一批发送的多条消息
        ///      提供同一个thirdparty_id, 便于友盟后台后期合并统计数据。 
        /// </summary>
        public string ThirdPartyId { get; set; }
    }
}