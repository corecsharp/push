using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Huawei
{
    public static class DicConverter
    {
        public static string ConvertToJson(this Dictionary<string, object> dic)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (var d in dic)
            {
                sb.Append("{\"" + d.Key + "\":\"" + d.Value.ToString() + "\"},");
            }
            if (sb[sb.Length - 1] == ',')
                sb.Remove(sb.Length - 2, 1);

            sb.Append("]");
            return sb.ToString();
        }

        public static string ConvertToParam(this Dictionary<string, object> dic)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var d in dic)
            {
                sb.Append(d.Key + "=" + d.Value.ToString() + "&");
            }
            return sb.ToString();
        }

        public static List<Dictionary<string, object>> ConvertToDicList(this Dictionary<string, object> dic)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (var d in dic)
            {
                var newDic = new Dictionary<string, object>();
                newDic.Add(d.Key, d.Value);
                list.Add(newDic);
            }
            return list;
        }
    }
}
