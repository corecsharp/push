using System;
using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace Push.Core.Infrastructure.Cache
{
    public class RedisQueueCache<T>
    {

        public IDatabase DataBase { get; set; }

        public ConnectionMultiplexer ConnectionMultiplexer { get; set; }

        /// <summary>
        /// 使用StackExchange.Redis.dll获取Redis数据库
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="port">端口号</param>
        /// <param name="dataBaseNO">数据库号</param>
        /// <returns></returns>
        public virtual void InitRedisDataBase(string server, int port, int dataBaseNO, string password = "")
        {
            string connectStr = string.IsNullOrWhiteSpace(password) ? string.Format("{0}:{1}", server, port) : string.Format("{0}:{1},password={2}", server, port, password);
            ConnectionMultiplexer = ConnectionMultiplexer.Connect(connectStr);
            IDatabase db = ConnectionMultiplexer.GetDatabase(dataBaseNO);
            DataBase = db;
        }
        /// <summary>
        /// 队列从右放
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public virtual long ListRightPush(string key, T val)
        {
            if (val == null) return 0;
            try
            {
                IDatabase db = DataBase;
                string valStr = JsonConvert.SerializeObject(val);
                return db.ListRightPush(key, valStr);
            }
            catch
            {
                return 0;
            }

        }
        /// <summary>
        /// 队列批量从右放
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public virtual long ListRightPush(string key, List<T> val)
        {
            if (val == null) return 0;
            try
            {
                IDatabase db = DataBase;
                RedisValue[] pushArray = new RedisValue[val.Count];
                for (int i = 0; i < val.Count; i++)
                {
                    pushArray[i] = JsonConvert.SerializeObject(val[i]);
                }
                return db.ListRightPush(key, pushArray);
            }
            catch
            {
                return 0;
            }

        }
        /// <summary>
        /// 队列从左出
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual T ListLeftPop(string key)
        {
            try
            {
                IDatabase db = DataBase;
                RedisValue redisValue = db.ListLeftPop(key);
                if (redisValue.IsNullOrEmpty) return default(T);
                T val = JsonConvert.DeserializeObject<T>(redisValue);
                return val;
            }
            catch
            {
                return default(T);
            }
        }

        public virtual long ListLength(string key)
        {
            IDatabase db = DataBase;
            return db.ListLength(key);
        }
    }
}
