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

namespace Push.Api.Controllers
{
    [Route("api/Push")]
    public class SendMsgController : SherlockApiController
    {
        private Lazy<ISendMsgService> _sendMsgServiceLazy = null;
        private Lazy<IProcessService> _processServiceLazy = null;
        private Lazy<ILoggerFactory> _loggerFactoryLazy = null;


        public SendMsgController()
        {
            _sendMsgServiceLazy = new Lazy<ISendMsgService>(() => WorkContext.Resolve<ISendMsgService>());
            _processServiceLazy = new Lazy<IProcessService>(() => WorkContext.Resolve<IProcessService>());
            _loggerFactoryLazy = new Lazy<ILoggerFactory>(() => WorkContext.Resolve<ILoggerFactory>());
        }

        /// <summary>
        /// 从Process表取出待发送的BatchNO
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetProcessBatchNO")]
        public object GetProcessBatchNO()
        {
            ILogger logger = _loggerFactoryLazy.Value.CreateLogger(nameof(SendMsgController));
            try
            {
                Stopwatch stop = new Stopwatch();
                stop.Start();
                string retMsg;
                string batchNO = _processServiceLazy.Value.GetProcessBatchNO(out retMsg);
                stop.Stop();
                logger.LogDebug("GetProcessBatchNO：" + batchNO + ",TimeSpan:" + stop.ElapsedMilliseconds.ToString());
                return this.Success(batchNO);
            }
            catch (Exception ex)
            {
                logger.LogDebug("GetProcessBatchNO：" + ex.Message);
                return this.Error(ErrCode.InnerError, ex.Message);
            }
        }

        /// <summary>
        /// 通过批次号，批量推送信息（提供给轮询服务使用）
        /// </summary>
        /// <param name="batchNO"></param>
        /// <returns></returns>
        [HttpPost, Route("SendProcessByBatchNO/{batchNO}")]
        public object SendProcessByBatchNO(string batchNO)
        {
            ILogger logger = _loggerFactoryLazy.Value.CreateLogger(nameof(SendMsgController));
            try
            {
                Stopwatch stop = new Stopwatch();
                stop.Start();
                object response;
                string retMsg;
                var ret = _sendMsgServiceLazy.Value.SendMsgListByBatchNO(batchNO, out retMsg);
                if (ret == ErrCode.Sucess)
                {
                    response = this.Success(true);
                }
                else
                {
                    response = this.Error(ret, retMsg);
                }
                stop.Stop();
                logger.LogDebug("SendProcessByBatchNO：" + batchNO + ",TimeSpan:" + stop.ElapsedMilliseconds.ToString());
                return response;
            }
            catch (Exception ex)
            {
                logger.LogDebug("SendProcessByBatchNO：" + ex);
                return this.Error(ErrCode.InnerError, ex.Message);
            }
        }
    }




}
