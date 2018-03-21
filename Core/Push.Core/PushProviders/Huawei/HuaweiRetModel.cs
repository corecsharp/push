using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Huawei
{
    public class GetAccessTokenRetModel
    {
        public string Access_Token { get; set; }
        public int Expires_In { get; set; }
    }
    public class PushRetModel
    {
        public string resultcode { get; set; }
        public string message { get; set; }
        public string requestID { get; set; }
        public string error { get; set; }
    }
}
