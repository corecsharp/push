using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.ConfigService.DBModel
{
    /// <summary>
    /// 推送配置
    /// </summary>
    public class PushConfig
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// 配置键
        /// </summary>
        public string ConfigKey { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        public string ConfigValue { get; set; }

        /// <summary>
        /// 配置排序索引
        /// </summary>
        public int ConfigIndex { get; set; }

        /// <summary>
        /// 配置描述
        /// </summary>
        public string ConfigDes { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive { get; set; }

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
        /// 更新人
        /// </summary>
        public long UpdateId { get; set; }


    }
}
