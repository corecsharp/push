using Platform.Core.Helper;
using Sherlock.Framework.Caching;
using System;
using System.Threading.Tasks;

namespace Platform.Core.Extentions
{
    public static class CacheExtentions
    {
        /// <summary>
        /// 异步查询数据使用缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key">缓存键</param>
        /// <param name="getData">数据获取逻辑</param>
        /// <param name="checkData">数据验证逻辑</param>
        /// <param name="expires">过期时间</param>
        /// <param name="useSlidingExpiration">是否使用滑动机制</param>
        /// <returns></returns>
        public static async Task<T> WithCacheQuery<T>(this ICacheManager cacheManager,
            string key,
            Func<Task<T>> getData,
            Predicate<T> checkData,
            TimeSpan expires,
            bool useSlidingExpiration = false)
        {
            var cache = cacheManager.GetWithRegion<T>(key);
            if (checkData(cache)) return cache;

            var data = await getData();
            if (checkData(data))
                cacheManager.SetWithRegion(key, data, expires, useSlidingExpiration);

            return data;
        }

        /// <summary>
        /// 同步查询数据使用缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManager"></param>
        /// <param name="key">缓存键</param>
        /// <param name="getData">数据获取逻辑</param>
        /// <param name="checkData">数据验证逻辑</param>
        /// <param name="expires">过期时间</param>
        /// <param name="useSlidingExpiration">是否使用滑动机制</param>
        /// <returns></returns>
        public static T WithCacheQuerySync<T>(this ICacheManager cacheManager,
            string key,
            Func<T> getData,
            Predicate<T> checkData,
            TimeSpan expires,
            bool useSlidingExpiration = false)
        {
            var cache = cacheManager.GetWithRegion<T>(key);
            if (checkData(cache)) return cache;

            var data = getData();
            if (checkData(data))
                cacheManager.SetWithRegion(key, data, expires, useSlidingExpiration);

            return data;
        }

        /// <summary>
        /// 使用配置的Region设置缓存
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="key">缓存键</param>
        /// <param name="data">数据</param>
        /// <param name="timeout">过期时间</param>
        /// <param name="useSlidingExpiration">是否使用滑动缓存</param>
        public static void SetWithRegion(this ICacheManager cacheManager, string key, object data, TimeSpan? timeout = null,
            bool useSlidingExpiration = false)
        {
            cacheManager.Set($"{ RedisCacheKeyHelper.RedisCacheKeyOptions.Region}:{ key}", data, timeout, useSlidingExpiration: useSlidingExpiration);
        }

        /// <summary>
        /// 使用配置的Region获取缓存
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="key">缓存键</param>
        public static T GetWithRegion<T>(this ICacheManager cacheManager, string key)
        {
            return cacheManager.Get<T>($"{ RedisCacheKeyHelper.RedisCacheKeyOptions.Region}:{ key}");
        }

        /// <summary>
        /// 使用配置的Region清除缓存
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="key">缓存键</param>
        public static void RemoveWithRegion(this ICacheManager cacheManager, string key)
        {
            cacheManager.Remove($"{ RedisCacheKeyHelper.RedisCacheKeyOptions.Region}:{ key}");
        }
    }
}
