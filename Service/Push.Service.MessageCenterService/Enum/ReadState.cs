using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Push.Service.MessageCenterService.Enum
{
    public enum ReadState 
    {
        [Description("未读")]
        UnRead = 0,

        [Description("已读")]
        Read = 1,

        [Description("清空")]
        Clear = 99
    }
}
