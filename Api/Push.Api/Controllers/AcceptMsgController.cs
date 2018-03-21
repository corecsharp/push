using Platform.Core.Filter;
using Push.Api.DTOs;
using Push.Api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sherlock.Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Push.Service.TokenService.Service;
using AutoMapper;
using System.Threading;
using Sherlock.Framework.Environment;

namespace Push.Api.Controllers
{
    [Route("api/Push")]
    public class AcceptMsgController : SherlockApiController
    {
        private Lazy<IMsgService> _msgServiceLazy = null;
        private Lazy<ILoggerFactory> _loggerFactoryLazy = null;
        private Lazy<IPushTokenBrandService> _pushTokenBrandServiceLazy = null;
        private Lazy<IMapper> _mapperLazy = null;

        public AcceptMsgController()
        {
            _msgServiceLazy = new Lazy<IMsgService>(() => WorkContext.Resolve<IMsgService>());
            _loggerFactoryLazy = new Lazy<ILoggerFactory>(() => WorkContext.Resolve<ILoggerFactory>());
            _pushTokenBrandServiceLazy = new Lazy<IPushTokenBrandService>(() => WorkContext.Resolve<IPushTokenBrandService>());
            _mapperLazy = new Lazy<IMapper>(() => WorkContext.Resolve<IMapper>());
        }

        /// <summary>
        /// 接受单条消息推送
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost, Route("Send"), ModelVerify]
        public object AcceptSingleMsg(SendMsgRequest req)
        {
            string retMsg;

            var ret = _msgServiceLazy.Value.AcceptMsgToTheardPool(req, out retMsg);
            var logger = _loggerFactoryLazy.Value.CreateLogger(nameof(AcceptMsgController));
            if (ret == ErrCode.Sucess)
            {
                logger.LogDebug("接受消息成功：" + Newtonsoft.Json.JsonConvert.SerializeObject(req));
                return this.Success(true);
            }
            logger.LogDebug("接受消息失败：" + retMsg);
            return this.Error(ret, retMsg);
        }



        /// <summary>
        /// 批量接收推送消息
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("SendList"), ModelVerify]
        public object AcceptMsgList(SendMsgListRequest req)
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();
            object response;
            string retMsg = string.Empty;
            try
            {
                var ret = _msgServiceLazy.Value.AcceptMsgList(req, out retMsg);
                if (ret == ErrCode.Sucess)
                    response = this.Success(true);
                else
                    response = this.Error(ret, retMsg);
            }
            catch (Exception ex)
            {
                response = this.Error(ErrCode.Failure, ex.Message.ToString());
            }
            stop.Stop();
            //LogHelper.Info.Write("SendList", "Time=" + DateTime.Now.ToString("HH:mm:ss,fff") + "\r\n" + resJson + "\r\n TimeSpan:" + stop.ElapsedMilliseconds.ToString());
            return response;
        }

        /// <summary>
        /// 获取消息队列中待推送的消息个数
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("QueueLength")]
        public object QueueLength()
        {
            int length = _msgServiceLazy.Value.GetQueueLength();
            return this.Success(length);
        }


        /// <summary>
        /// 推送全部用户（活动推广）
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("SendAll"), ModelVerify]
        public object AcceptMsgAll(SendMsgDto dto)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(SendAllAsync), dto);
            return this.Success("接收成功"); ;
        }

        /// <summary>
        /// 将消息发送给所有用户
        /// </summary>
        /// <param name="o"></param>
        private async void SendAllAsync(object o)
        {
            using (var scope = SherlockEngine.Current.CreateScope())
            {
                ILoggerFactory loggerFactory = scope.ServiceProvider.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
                var logger = loggerFactory.CreateLogger<AcceptMsgController>();
                SendMsgDto dto = o as SendMsgDto;
                //获取App下所有的用户
                IPushTokenBrandService pushTokenBrandService =scope.ServiceProvider.GetService(typeof(IPushTokenBrandService)) as IPushTokenBrandService;
                //IPushTokenBrandService pushTokenBrandService = SherlockEngine.Current.GetService<IPushTokenBrandService>();//这样写不会Dispose资源
                var allTokenList = await pushTokenBrandService.GetAllTokenBrand(dto.AppId);
                if (allTokenList == null || allTokenList.Count == 0)
                {
                    return;
                }
                IMapper mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var req = mapper.Map<SendMsgListRequest>(dto);
                req.TokenList = allTokenList;
                string retMsg = string.Empty;
                try
                {
                    //将消息拆解存到消息队列中
                    IMsgService msgService = scope.ServiceProvider.GetService(typeof(IMsgService)) as IMsgService;
                    //IMsgService msgService = SherlockEngine.Current.GetService<IMsgService>();
                    var ret = msgService.AcceptAllMsgList(req, out retMsg);
                    if (ret == ErrCode.Sucess)
                    {
                        logger.LogInformation("推送全体成功");
                    }
                    else
                    {
                        logger.LogInformation("推送全体失败：" + retMsg);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogInformation("推送全体失败：" + ex.Message.ToString());
                }
            }
        }

    }
}
