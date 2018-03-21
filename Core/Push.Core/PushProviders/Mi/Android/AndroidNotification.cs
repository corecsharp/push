using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Mi.Android
{
    public abstract class AndroidNotification : MiPushNotification
    {
        /// <summary>
        /// 单播使用
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static StringBuilder newBody(string name, string value)
        {
            StringBuilder body = new StringBuilder();
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(value))
            {
                return body;
            }
            return body.Append(name).Append('=').Append(value);
        }

        /// <summary>
        /// 单播使用
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void addParameter(StringBuilder body, string name, string value)
        {

            if (body == null)
            {
                body = new StringBuilder();
            }
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(value))
            {
                return;
            }
            body.Append('&').Append(name).Append('=').Append(value);
        }
    }
}
