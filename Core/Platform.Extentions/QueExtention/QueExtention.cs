using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sherlock.Framework;

namespace Platform.Extentions.QueExtention
{
    public static class QueExtention<T>
    {
        private static ConcurrentQueue<T> queue;

        public static ConcurrentQueue<T> Queue
        {
            get { return queue; }
            set { queue = value; }
        }

        public static void CreateQue()
        {
            Queue = new ConcurrentQueue<T>();
        }

        /// <summary>
        /// 单条入队
        /// </summary>
        /// <param name="t"></param>
        public static void SingleEnqueue(T t)
        {
            if (Queue != null)
                Queue.Enqueue(t);
        }

        /// <summary>
        /// 多条入队
        /// </summary>
        public static void MultipleEnqueue(ICollection<T> collection)
        {
            if (Queue != null&& collection!=null)
                collection.ForEach(p => Queue.Enqueue(p));
        }

        /// <summary>
        /// 单条出队
        /// </summary>
        /// <param name="t"></param>
        public static T SingleDequeue(T t)
        {
            if (Queue != null&& Queue.Count>0)
                Queue.TryDequeue(out t);
            return t;
        }

        /// <summary>
        /// 多条出队
        /// </summary>
        /// <param name="t"></param>
        public static ICollection<T> MultipleDequeue(ICollection<T> collection,int Count=0) 
        {
            if (Queue.Count > 0)
            {
                var length = Queue.Count;
                for (int i = 0; i < length; i++)
                {
                    if(Count!=0&&collection.Count==Count)
                        return collection;
                    T source;
                    Queue.TryDequeue(out source);
                    if (source != null)
                        collection.Add(source);
                }
            }
            return collection;
        }
    }
}
