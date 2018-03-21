using Platform.Core.Options;
using Microsoft.Extensions.Options;
using Sherlock.Framework;
using Sherlock.Framework.Environment;

namespace Platform.Core.Helper
{

    public class RedisCacheKeyHelper
    {
        public static RedisCacheKeyOptions RedisCacheKeyOptions => SherlockEngine.Current.GetService<IOptionsSnapshot<RedisCacheKeyOptions>>().Value;

        public static void AddRedisCacheKeyOptions(RedisCacheKeyOptions redisCacheKeyOptions)
        {
            //RedisCacheKeyOptions = redisCacheKeyOptions;
        }
    }

    
}
