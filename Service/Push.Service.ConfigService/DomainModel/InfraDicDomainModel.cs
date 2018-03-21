using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.ConfigService.DomainModel
{
    /// <summary>
    ///  字典
    /// </summary>
    public class InfraDicDomainModel
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 字典表Key值
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// 字典Value值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 字典Type值
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime CreateAt { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateAt { get; set; }

     
    }
}
