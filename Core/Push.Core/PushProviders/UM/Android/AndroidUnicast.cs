using Push.Core.DTOs.UM;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.UM.Android
{
    public class AndroidUnicast : AndroidNotification
    {
        public AndroidUnicast()
        {
        }
        public override string InitPushUrl(object obj)
        {
            var pushdto = obj as AndroidPushDto;
            if (pushdto == null)
            {
                return string.Empty;
            }

            return pushdto.Url;
        }
        public override string InitAppMasterSecret(object obj)
        {
            var pushdto = obj as AndroidPushDto;
            if (pushdto == null)
            {
                return string.Empty;
            }

            return pushdto.AppMasterSecret;
        }
        public override JObject InitUmMessage(object obj)
        {
            var pushdto = obj as AndroidPushDto;
            if (pushdto == null)
            {
                return null;
            }

            var objson = new JObject();
            // this.AppMasterSecret = pushdto.AppMasterSecret;
            this.setPredefinedKeyValue(objson, "type", "unicast");
            this.setPredefinedKeyValue(objson, "appkey", pushdto.AppKey);
            this.setPredefinedKeyValue(objson, "timestamp", pushdto.TimeStamp);
            // TODO Set your device token
            this.setPredefinedKeyValue(objson, "device_tokens", pushdto.DeviceTokens);
            this.setPredefinedKeyValue(objson, "ticker", pushdto.Ticker);
            this.setPredefinedKeyValue(objson, "title", pushdto.Title);
            this.setPredefinedKeyValue(objson, "text", pushdto.Text);
            this.setPredefinedKeyValue(objson, "after_open", pushdto.AfterOpen.ToString());
            this.setPredefinedKeyValue(objson, "display_type", pushdto.Displaytype.ToString());
            this.setPredefinedKeyValue(objson, "description", pushdto.Description);
            // TODO Set 'production_mode' to 'false' if it's a test device. 
            // For how to register a test device, please see the developer doc.
            this.setPredefinedKeyValue(objson, "production_mode", pushdto.ProductionMode.ToString().ToLower());
            // Set customized fields
            if (pushdto.Extra != null && pushdto.Extra.Count > 0)
            {
                foreach (var item in pushdto.Extra.Keys)
                {
                    this.setExtraField(objson, item, pushdto.Extra[item]);
                }
            }

            return objson;
        }
    }
}
