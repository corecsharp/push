using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.DTOs
{
    /// <summary>
    ///  品牌和通道的关系
    /// </summary>
    public class BrandChannelDto : CourseBaseDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public  int Id { get; set; }

        /// <summary>
        /// 品牌编号（品牌存于dic表中）
        /// </summary>
        public  int BrandId { get; set; }

        /// <summary>
        /// 通道Id
        /// </summary>
        public  long ChannelId { get; set; }
    }
}
