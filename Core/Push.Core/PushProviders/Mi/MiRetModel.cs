using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Mi
{
    public class MiRetModel
    {
        public string result { get; set; }

        public string reason { get; set; }

        public string trace_id { get; set; }

        public string code { get; set; }

        public RetResult data { get; set; }
        public string description { get; set; }

        public string info { get; set; }
    }

    /// <summary>
    /// 当发送成功，会生成id
    /// </summary>
    public class RetResult
    {
        public string id { get; set; }

    }
}
