using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Push.Core.Infrastructure.Enum
{
    public static class EnumHelper
    {

        /// <summary>
        /// 获取描述信息
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns></returns>
        public static string GetEnumDes(this System.Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                IEnumerable<Attribute> attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null)
                {
                    var attrsList = attrs.ToList();
                    if (attrsList.Count > 0)
                    {
                        return ((DescriptionAttribute)attrsList[0]).Description;
                    }
                }
            }
            return en.ToString();
        }

    }
}
