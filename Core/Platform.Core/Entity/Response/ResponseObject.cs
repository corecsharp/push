using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Core.Entity.Response
{
    public class ResponseObject<T>
    {
        public string Msg { get; set; }

        public string Code { get; set; }

        public T Data { get; set; }
    }

    
}
