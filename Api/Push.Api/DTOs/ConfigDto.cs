using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.DTOs
{
    /// <summary>
    ///  配置信息
    /// </summary>
    public class ConfigDto : CourseBaseDto
    {
        /// 主键
        /// </summary>
        public  int Id { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        public  string ConfigName { get; set; }

        /// <summary>
        /// 配置的Key
        /// </summary>
        public  string ConfigKey { get; set; }

        /// <summary>
        /// 配置的值
        /// </summary>
        public  string ConfigValue { get; set; }

        /// <summary>
        /// 配置顺序
        /// </summary>
        public  int ConfigIndex { get; set; }

        /// <summary>
        /// 配置描述
        /// </summary>
        public  string ConfigDes { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public  bool IsActive { get; set; }
    }
}
