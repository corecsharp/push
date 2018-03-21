using Sherlock.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Core.Options
{
    [ConfiguredOptions("RedisCacheKeyOptions")]
    public class RedisCacheKeyOptions
    {
        public string Region { get; set; }
    }
}
