using Push.Core.PushProviders.Mi.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.Mi
{
    public class PushApplicationSession : IPushApplicationSession
    {
        private INotifyApplicationService _IPushAndroidUnicast;
        public INotifyApplicationService IPushAndroidUnicast
        {
            get
            {
                if (_IPushAndroidUnicast == null)
                {
                    _IPushAndroidUnicast = new AndroidUnicast();
                }
                return _IPushAndroidUnicast;
            }
        }

        private INotifyApplicationService _IPushAndroidMulticast;
        public INotifyApplicationService IPushAndroidMulticast
        {
            get
            {
                if (_IPushAndroidMulticast == null)
                {
                    _IPushAndroidMulticast = new AndroidMulitcast();
                }
                return _IPushAndroidMulticast;
            }
        }


    }
}
