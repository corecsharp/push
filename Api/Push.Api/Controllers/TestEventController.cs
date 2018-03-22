//using Microsoft.AspNetCore.Mvc;
//using Sherlock.Framework.Events;
//using Sherlock.Framework.Web.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Push.Api.Controllers
//{
//    /// <summary>
//    /// 测试事件通知
//    /// </summary>
//    [Route("api/Event")]
//    public class TestEventController : SherlockApiController
//    {
//        private IEventNotification _eventNotification;

//        public TestEventController(IEventNotification eventNotification)
//        {
//            _eventNotification = eventNotification;
//        }

//        /// <summary>
//        /// 本地触发事件
//        /// </summary>
//        /// <param name="req"></param>
//        /// <returns></returns>
//        [HttpGet]
//        public object BrandAsync()
//        {
//            //Event.AyncEvent.Do();
//            //object c = new { a = "A" };
//            Console.WriteLine("触发事件：" + DateTime.Now);
//            //ThreadPool.QueueUserWorkItem(new WaitCallback(Accept), c);
//            _eventNotification.Notify("TestSherlockNotify",null,null);
//            return this.Success("调用成功");
//        }

      

//        public void Accept(object o)
//        {
//            for (int i = 0; i < 100; i++)
//            {
//                Console.WriteLine(i);
//                System.Threading.Thread.Sleep(1000);
//            }
//        }
//    }


//    public interface ITestEventService: Sherlock.Framework.IDependency
//    {


//    }

//    public class TestEventService : ITestEventService
//    {
//        /// <summary>
//        /// Sherlock事件
//        /// </summary>
//        [EventSubscription("TestSherlockNotify")]
//        public void TestSherlockNotify(object sender, object args)
//        {
//            Console.Write("接受事件：" + DateTime.Now);
//        }
//    }
//}
