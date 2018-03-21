using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.UM
{
    public class UMRetModel
    {
        public string ret { get; set; }

        public RetResult data { get; set; }
    }

    public class RetResult
    {
        /// <summary>
        /// ret为SUCCESS时，msg_id有值
        /// </summary>
        public string msg_id { get; set; }
        /// <summary>
        /// ret为FAIL时，error_code有值
        /// </summary>
        public string error_code { get; set; }

        public string appkey { get; set; }
        public string ip { get; set; }
    }
}
