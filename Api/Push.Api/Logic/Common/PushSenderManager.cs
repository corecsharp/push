using Push.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.Logic.Common
{
    public class PushSenderManager
    {
        static Dictionary<long, ISender> _senderDic = new Dictionary<long, ISender>();

        public static void AddOrUpdateSender(long providerId, ISender sender)
        {
            if (_senderDic.ContainsKey(providerId))
                _senderDic[providerId] = sender;
            else
                _senderDic.Add(providerId, sender);
        }
        public static ISender GetSender(long providerId)
        {
            ISender sender;
            if (!_senderDic.TryGetValue(providerId, out sender))
                return null;
            return sender;
        }
    }
}
