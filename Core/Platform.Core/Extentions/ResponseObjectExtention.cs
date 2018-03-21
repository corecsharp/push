using Platform.Core.Entity.Response;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Platform.Core.Extentions
{
    public static class ResponseObjectExtention
    {
        public static ResponseObject<T> IncludeResponse<T>(this T t,string msg = null,string code = null)
        {
            var response = new ResponseObject<T>();
            response.Msg = msg;
            response.Code = code;
            response.Data = t;
            return response;
        }

        public static ErrDetail GetErrDefinition<T>(this T t)
            where T:struct
        {
            var type = typeof(T).GetTypeInfo();
            if (!type.IsEnum)
            {
                throw new InvalidCastException("传入参数不是枚举类型");
            }
            var detail = new ErrDetail();
            detail.Msg = t.ToString();
            detail.Code = Convert.ToInt64(t).ToString();
            MemberInfo[] memInfo = type.GetMember(detail.Msg);
            if (memInfo != null && memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null)
                {
                    List<Attribute> attributeList = attrs.Select(e=>(Attribute)e).ToList();
                    if (attributeList.Count > 0) {
                        detail.Msg = ((DescriptionAttribute)attributeList[0]).Description;
                    }
                }
            }
            return detail;
        }

    }
}
