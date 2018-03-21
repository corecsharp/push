using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.UM.Android
{
    public abstract class AndroidNotification : UmengNotification
    {
        // Keys can be set in the payload level
        protected internal static readonly HashSet<string> PAYLOAD_KEYS = new HashSet<string>((new string[] { "display_type" }).ToList());

        // Keys can be set in the body level
        protected internal static readonly HashSet<string> BODY_KEYS = new HashSet<string>((new string[] { "ticker", "title", "text", "builder_id", "icon", "largeIcon", "img", "play_vibrate", "play_lights", "play_sound", "sound", "after_open", "url", "activity", "custom" }).ToList());

        // Set key/value in the rootJson, for the keys can be set please see ROOT_KEYS, PAYLOAD_KEYS, 
        // BODY_KEYS and POLICY_KEYS.
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: @Override public boolean setPredefinedKeyValue(String key, Object value) throws Exception
        public override bool setPredefinedKeyValue(JObject rootJson, string key, string value)
        {
            if (ROOT_KEYS.Contains(key))
            {
                // This key should be in the root level
                rootJson.Add(key, value);
            }
            else if (PAYLOAD_KEYS.Contains(key))
            {
                // This key should be in the payload level
                JObject payloadJson = null;

                if (rootJson.Property("payload") != null)
                {
                    payloadJson = rootJson.GetValue("payload") as JObject;
                }
                else
                {
                    payloadJson = new JObject();
                    rootJson.Add("payload", payloadJson);
                }
                payloadJson.Add(key, value);
            }
            else if (BODY_KEYS.Contains(key))
            {
                // This key should be in the body level
                JObject bodyJson = null;
                JObject payloadJson = null;
                // 'body' is under 'payload', so build a payload if it doesn't exist
                if (rootJson.Property("payload") != null)
                {
                    payloadJson = rootJson.GetValue("payload") as JObject;
                }
                else
                {
                    payloadJson = new JObject();
                    rootJson.Add("payload", payloadJson);
                }
                // Get body JsonObject, generate one if not existed
                if (payloadJson.Property("body") != null)
                {
                    bodyJson = payloadJson.GetValue("body") as JObject;
                }
                else
                {
                    bodyJson = new JObject();
                    payloadJson.Add("body", bodyJson);
                }
                bodyJson.Add(key, value);
            }
            else if (POLICY_KEYS.Contains(key))
            {
                // This key should be in the body level
                JObject policyJson = null;
                if (rootJson.Property("policy") != null)
                {
                    policyJson = rootJson.GetValue("policy") as JObject;
                }
                else
                {
                    policyJson = new JObject();
                    rootJson.Add("policy", policyJson);
                }
                policyJson.Add(key, value);
            }
            else
            {
                if (key == "payload" || key == "body" || key == "policy" || key == "extra")
                {
                    throw new Exception("You don't need to set value for " + key + " , just set values for the sub keys in it.");
                }
                else
                {
                    throw new Exception("Unknown key: " + key);
                }
            }
            return true;
        }

        // Set extra key/value for Android notification
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public boolean setExtraField(String key, String value) throws Exception
        public virtual bool setExtraField(JObject rootJson, string key, string value)
        {
            JObject payloadJson = null;
            JObject extraJson = null;
            if (rootJson.Property("payload") != null)
            {
                payloadJson = rootJson.GetValue("payload") as JObject;
            }
            else
            {
                payloadJson = new JObject();
                rootJson.Add("payload", payloadJson);
            }

            if (payloadJson.Property("extra") != null)
            {
                extraJson = payloadJson.GetValue("extra") as JObject;
            }
            else
            {
                extraJson = new JObject();
                payloadJson.Add("extra", extraJson);
            }
            extraJson.Add(key, value);
            return true;
        }
    }
}
