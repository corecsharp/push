using Push.Core.DTOs.UM;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.UM.IOS
{
    public class IOSUnicast : IOSNotification
    {
        public IOSUnicast()
        {

        }

        public override string InitPushUrl(object obj)
        {
            var pushdto = obj as IOSPushDto;
            if (pushdto == null)
            {
                return string.Empty;
            }

            return pushdto.PushUrl;
        }

        public override string InitAppMasterSecret(object obj)
        {
            var pushdto = obj as IOSPushDto;
            if (pushdto == null)
            {
                return string.Empty;
            }

            return pushdto.AppMasterSecret;
        }



        public override JObject InitUmMessage(object obj)
        {
            var pushdto = obj as IOSPushDto;
            if (pushdto == null)
            {
                return null;
            }


            var objson = new JObject();

            this.setPredefinedKeyValue(objson, "type", "unicast");

            this.setPredefinedKeyValue(objson, "appkey", pushdto.AppKey);
            this.setPredefinedKeyValue(objson, "timestamp", pushdto.TimeStamp);
            // TODO Set your device token
            this.setPredefinedKeyValue(objson, "device_tokens", pushdto.DeviceTokens);
            this.setPredefinedKeyValue(objson, "alert", pushdto.Alert);
            this.setPredefinedKeyValue(objson, "badge", pushdto.Badge.ToString());
            this.setPredefinedKeyValue(objson, "sound", pushdto.Sound);
            // TODO set 'production_mode' to 'true' if your app is under production mode
            this.setPredefinedKeyValue(objson, "production_mode", pushdto.ProductionMode.ToString().ToLower());
            this.setPredefinedKeyValue(objson, "description", pushdto.Description);
            if (pushdto.Extra != null && pushdto.Extra.Count > 0)
            {
                foreach (var item in pushdto.Extra.Keys)
                {
                    this.setCustomizedField(objson, item, pushdto.Extra[item]);
                }
            }
            return objson;
        }

    }
}
