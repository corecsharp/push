using Platform.Core.Options;
using Push.Core.Infrastructure.Cache;
using Microsoft.Extensions.Options;
using Sherlock.Framework.Environment;
using System;
using System.Collections.Generic;

namespace Push.Api.Logic
{
    /// <summary>
    /// Redis队列逻辑
    /// </summary>
    public class RedisQueueLogic<T> : IQueueLogic<T>
    {
        public static RedisQueueCache<T> queue;
        const string TAG = "RedisQueueLogic";
        /// <summary>
        /// 是否使用本地RedisQueue
        /// </summary>
        private static bool _isUseRedisQueue = true;

        public static string RedisQueueKey { get; set; }//队列Key值

        static RedisQueueLogic()
        {
            RedisConnectOptions redisConnectOptions = SherlockEngine.Current.GetService<IOptionsSnapshot<RedisConnectOptions>>().Value;
            //如果Redis在使用,将队列的Key值附上
            if (RedisQueueKey.IsNullOrWhiteSpace())
            {
                RedisCacheKeyOptions redisCacheKeyOptions = SherlockEngine.Current.GetService<IOptionsSnapshot<RedisCacheKeyOptions>>().Value;
                RedisQueueKey = $"{redisCacheKeyOptions.Region}:Msg:Queue";
            }
            if (redisConnectOptions==null)
            {
                _isUseRedisQueue = false;
            }
            else
            {
                queue = new RedisQueueCache<T>();
                try
                {
                    queue.InitRedisDataBase(redisConnectOptions.Server, redisConnectOptions.Port, redisConnectOptions.DataBaseNo, redisConnectOptions.Password);
                    //LogHelper.Info.Write(TAG, string.Format("RedisServer:{0},RedisPassword={1},初始化成功", redisServer, redisPassword));
                }
                catch (Exception ex)
                {
                    _isUseRedisQueue = false;
                    //LogHelper.Error.Write(TAG, string.Format("RedisServer:{0},RedisPassword={1},初始化失败", redisServer, redisPassword));
                }
            }
        }

        public bool IsUseRedisQueue
        {
            get
            {
                return _isUseRedisQueue;
            }
        }

        /// <summary>
        /// 进队列
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void Enqueue(T obj)
        {
            queue.ListRightPush(RedisQueueKey, obj);
        }

        //批量进队列
        public void Enqueue(List<T> list)
        {
            queue.ListRightPush(RedisQueueKey, list);
        }

        //出队列
        public T Dequeue()
        {
            return queue.ListLeftPop(RedisQueueKey);
        }

        //队列长度
        public int Length() {
            return (int)queue.ListLength(RedisQueueKey);
        }

    }
}
