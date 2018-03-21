using Platform.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.DTOs
{
    /// <summary>
    /// 进程基类
    /// </summary>
    [Serializable]
    public class CourseBaseDto
    {
        public CourseBaseDto()
        {
            UpdateAt = DateTimeHelper.GetNow();
        }
        //更新时间
        public DateTime CreateAt { get; set; }
        //数据加载时间
        public DateTime UpdateAt { get; set; }
     
    }


    [Serializable]
    public class CourseBaseDto<T> : CourseBaseDto where T : new()
    {
        public T Content { get; set; }
    }
}
