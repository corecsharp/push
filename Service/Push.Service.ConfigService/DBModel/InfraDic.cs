using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.ConfigService.DBModel
{
    /// <summary>
    /// 字典信息
    /// </summary>
    public class InfraDic
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        public long Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 类型。appid,brandid
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateAt { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreateId { get; set; }

        /// <summary>
        /// 跟新人
        /// </summary>
        public long UpdateId { get; set; }


    }
}
