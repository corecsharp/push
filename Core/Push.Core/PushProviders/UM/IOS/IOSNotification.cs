using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.PushProviders.UM.IOS
{
    public abstract class IOSNotification : UmengNotification
    {

        // Keys can be set in the aps level
        protected internal static readonly HashSet<string> APS_KEYS = new HashSet<string>((new string[] { "alert", "badge", "sound", "content-available" }).ToList());

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: @Override public boolean setPredefinedKeyValue(String key, Object value) throws Exception
        public override bool setPredefinedKeyValue(JObject rootJson, string key, string value)
        {
            if (ROOT_KEYS.Contains(key))
            {
                // This key should be in the root level
                rootJson.Add(key, value);
            }
            else if (APS_KEYS.Contains(key))
            {
                // This key should be in the aps level
                JObject apsJson = null;
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
                if (payloadJson.Property("aps") != null)
                {
                    apsJson = payloadJson.GetValue("aps") as JObject;
                }
                else
                {
                    apsJson = new JObject();
                    payloadJson.Add("aps", apsJson);
                }
                apsJson.Add(key, value);
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
                if (key == "payload" || key == "aps" || key == "policy")
                {
                    throw new Exception("You don't need to set value for " + key + " , just set values for the sub keys in it.");
                }
                else
                {
                    throw new Exception("Unknownd key: " + key);
                }
            }

            return true;
        }
        // Set customized key/value for IOS notification
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public boolean setCustomizedField(String key, String value) throws Exception
        public virtual bool setCustomizedField(JObject rootJson, string key, string value)
        {
            //rootJson.put(key, value);
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
            return true;
        }

    }
}
