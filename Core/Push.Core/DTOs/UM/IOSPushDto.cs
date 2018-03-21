using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.DTOs.UM
{
    public class IOSPushDto
    {
        /// <summary>
        ///推送的URL
        /// </summary>
        public string PushUrl { get; set; }
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
        /// 必填 显示内容
        /// </summary>
        public string Alert { get; set; }

        /// <summary>
        /// 可选 徽记，就是应用程序上显示的小红数字提示有几条信息用
        /// </summary>
        public int Badge { get; set; }

        /// <summary>
        /// 可选 声音
        /// </summary>
        public string Sound { get; set; }

        /// <summary>
        /// 可选 1表示后台推送，不写表示普通推送
        /// </summary>
        public int ContentAvailable { get; set; }
        /// <summary>
        /// 可选 ios8才支持
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 可选 用户自定义key-value
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
