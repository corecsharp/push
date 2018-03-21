using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace System
{
    public static class EnumExtention
    {
        public static string ToString<TEnum>(this int val)
            where TEnum : struct
        {
            var type = typeof(TEnum).GetTypeInfo();
            if (!type.IsEnum)
            {
                throw new InvalidCastException("传入参数不是枚举类型");
            }
            var enumval = Enum.GetName(typeof(TEnum), val);
            return enumval;
        }
    }
}
