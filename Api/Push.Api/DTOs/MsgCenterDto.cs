using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Api.DTOs
{
    public class MsgTypeListRequestDto
    {
        /// <summary>
        /// app标识
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// App中用户id
        /// </summary>
        public long UserId { get; set; }
    }

    public class MsgListResponseDto
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 附加信息
        /// </summary>
        public string AttachInfo { get; set; }

        /// <summary>
        /// 是否已读状态
        /// </summary>
        public bool IsRead { get; set; }


    }



    public class MsgListRequestDto
    {
        /// <summary>
        /// app标识
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// App中用户id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 消息类型，用户自定义
        /// </summary>
        public int MsgType { get; set; }
    }

    public class ReadMsgRequestDto
    {

        /// <summary>
        /// 消息类型
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// app标识
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// App中用户id
        /// </summary>
        public long UserId { get; set; }
    }

    public class BatchReadMsgRequestDto
    {
        /// <summary>
        /// 消息ids
        /// </summary>
        public long[] Ids { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public virtual long UserId { get; set; }

        /// <summary>
        /// app标识
        /// </summary>
        public virtual int AppId { get; set; }
    }

    public class UnReadCountRequestDto
    {
        /// <summary>
        /// app标识
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// App中用户id
        /// </summary>
        public long UserId { get; set; }
    }

    public class ClearMsgRequestDto
    {
        /// <summary>
        /// app标识
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// App中用户id
        /// </summary>
        public long UserId { get; set; }


    }

}
