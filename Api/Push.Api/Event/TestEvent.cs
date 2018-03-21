using Sherlock.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.Event
{
    public class Mom
    {
        public event Action Eat;

        public void Cook()
        {
            Console.WriteLine("妈妈 : 饭好了");
            Eat();
        }
    }

    public class Family
    {
        public void Eat()
        {
            System.Threading.Thread.Sleep(10000);
            Console.WriteLine("家里人：吃饭");
        }
    }

    public class AyncEvent
    {

        //定义一个事件
        public static event EventHandler<EventArgs> OnEvent;
        //方法1
        public static void Method1(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(10000);
            //显示执行该方法的线程ID
            Console.WriteLine("调用Method1的线程ID为：{0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
           
        }

        public static void Method2(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(10000);
            Console.WriteLine("调用Method2的线程ID为：{0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
        }

       public static void Do()
        {
            //显示主线程ID
            System.Console.WriteLine("主线程ID为：{0}", System.Threading.Thread.CurrentThread.ManagedThreadId);

            //将Method1和Method2注册到事件中
            OnEvent += new EventHandler<EventArgs>(Method1);
            OnEvent += new EventHandler<EventArgs>(Method2);

            //下面的代码实现事件的异步调用
            //获取事件中的多路委托列表
            Delegate[] delegAry = OnEvent.GetInvocationList();
            //遍历委托列表
            foreach (EventHandler<EventArgs> deleg in delegAry)
            {
                //异步调用委托
                deleg.BeginInvoke(null, EventArgs.Empty, null, null);
            }

            //System.Console.ReadKey();
        }
    }

}
