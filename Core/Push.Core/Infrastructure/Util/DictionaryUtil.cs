using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.Infrastructure.Util
{
    public class DictionaryUtil<TKey, TValue>
    {
        public static Dictionary<TKey, TValue> StringToDictionary(string dictionaryStr)
        {
            if (string.IsNullOrEmpty(dictionaryStr)) return null;
            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
            try
            {
                dictionary = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(dictionaryStr);
            }
            catch
            {
                dictionary = null;
                //LogHelper.Info.Write("ObjectToDictionary", string.Format("{0}，内容不符合规范，序列化失败，置为空继续发送", dictionaryStr));
            }
            return dictionary;
        }
    }
}
