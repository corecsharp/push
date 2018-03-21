using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Push.Core.Infrastructure
{
    public class SenderRet
    {
        /// <summary>
        /// 单挑消息的Id(目前单独给友盟使用)
        /// </summary>
        public long Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }
        public string Sign { get; set; }
        public string Code { get; set; }
        public List<SenderRet> ResultList { get; set; }
    }
}
