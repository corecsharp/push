using System.Collections.Generic;

namespace Push.Api.Logic
{
    /// <summary>
    /// 本地队列逻辑
    /// </summary>
    public class LocalQueueLogic<T> : IQueueLogic<T>
    {
        public static Queue<T> queue;

        static LocalQueueLogic()
        {
            queue = new Queue<T>();
        }

        /// <summary>
        /// 进队列
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void Enqueue(T obj)
        {
            queue.Enqueue(obj);
        }

        /// <summary>
        /// 批量进队列
        /// </summary>
        /// <param name="list"></param>
        public void Enqueue(List<T> list)
        {
            if (list == null) return;
            foreach (var item in list)
            {
                Enqueue(item);
            }
        }
        //出队列
        public T Dequeue()
        {
            try
            {
                return queue.Dequeue();
            }
            catch
            {
                return default(T);
            }
        }

        //队列长度
        public int Length()
        {
            return queue.Count;
        }

    }
}
