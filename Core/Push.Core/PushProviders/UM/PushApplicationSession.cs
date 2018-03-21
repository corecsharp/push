using Push.Core.PushProviders.UM.Android;
using Push.Core.PushProviders.UM.IOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.UM
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

        private INotifyApplicationService _IPushIOSUnicast;
        public INotifyApplicationService IPushIOSUnicast
        {
            get
            {
                if (_IPushIOSUnicast == null)
                {
                    _IPushIOSUnicast = new IOSUnicast();
                }
                return _IPushIOSUnicast;
            }
        }
    }
}
