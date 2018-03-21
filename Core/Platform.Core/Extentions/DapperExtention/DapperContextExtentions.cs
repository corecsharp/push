using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sherlock.Framework.Data
{
    public static class DapperContextExtentions
    {
        /// <summary>
        /// 按照映射策略获取类型名所映射的表名或属性名所映射的列名
        /// </summary>
        /// <param name="name">类型名或者属性名</param> 
        /// <returns></returns>
        public static string ConvertNameFromPascalToUnderline(this DapperContext context, string name)
        {
            var array = name.ToCharArray();
            var length = array.Length;
            var builder = new StringBuilder().Append(array[0]);
            var position = -1;
            for (int i = 1; i < length; i++)
            {
                var current = array[i];
                var prev = array[i - 1];
                if (char.IsUpper(current))
                {
                    if (char.IsLower(prev))
                    {
                        builder.Append("_").Append(current);
                        position = -1;
                    }
                    else
                    {
                        builder.Append(current);
                        position = i;
                    }
                }
                else
                {
                    builder.Append(current);
                }
            }
            if (position > 0)
            {
                builder.Insert(position - 1, "_");
            }
            return builder.ToString().ToLower();
        }
    }
}
