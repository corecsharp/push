using System;
using System.Collections.Generic;

namespace Push.Api.DTOs
{
    public class SendMsgListRequest : SendMsgDto
    {
        /// <summary>
        ///Token集合（业务中的userId的结合）
        /// </summary>
        public List<string> TokenList { get; set; }

    }
}
