using Sherlock.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.Logic
{
    public interface IQueueLogic<T>
    {
        //进队列
        void Enqueue(T obj);

        //批量进队列
        void Enqueue(List<T> list);

        //出队列
        T Dequeue();

        //队列长度
        int Length();

    }
}
