using Microsoft.Extensions.Options;
using Sherlock;
using Sherlock.Framework.Caching;
using System;

namespace Platform.Core.Extentions
{
    public class SessionService: ISessionService
    {
        private readonly ICacheManager _cache;
        private readonly GZSessionOptions _userSessionOptions;
        public SessionService(ICacheManager cache, IOptionsSnapshot<GZSessionOptions> userSessionOptions)
        {
            Guard.ArgumentNotNull(cache, nameof(cache));
            Guard.ArgumentNotNull(userSessionOptions, nameof(userSessionOptions));
            _cache = cache;
            _userSessionOptions = userSessionOptions.Value;
        }

        public void SetSession<T>(string token, T session, TimeSpan expireTime = default(TimeSpan))
        {
            Guard.ArgumentNotNull(token, nameof(token));
            Guard.ArgumentNotNull(session, nameof(session));
            var expireTimespan = expireTime == default(TimeSpan) ? TimeSpan.FromMinutes(_userSessionOptions.Duration) : expireTime;
            _cache.SetWithRegion(GetKey(token), session, expireTimespan,_userSessionOptions.Sliding);
        }

        public T GetSession<T>(string token)
        {
            var userSession = _cache.GetWithRegion<T>(GetKey(token));
            return userSession;
        }
        

        public void RemoveToken(string token)
        {
            _cache.RemoveWithRegion(GetKey(token));
        }

        private string GetKey(string token)
        {
            return $"{_userSessionOptions.Prefix}:{token}";
        }
    }
}
