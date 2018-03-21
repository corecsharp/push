using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.ConfigService.DomainModel
{
    public class PushConfigDomainModel
    {
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        public string ConfigName { get; set; }

        /// <summary>
        /// 配置的Key
        /// </summary>
        public string ConfigKey { get; set; }

        /// <summary>
        /// 配置的值
        /// </summary>
        public string ConfigValue { get; set; }

        /// <summary>
        /// 配置顺序
        /// </summary>
        public int ConfigIndex { get; set; }

        /// <summary>
        /// 配置描述
        /// </summary>
        public string ConfigDes { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsActive { get; set; }
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
