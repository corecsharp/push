using Push.Api.DTOs;
using Push.Api.Service;
using Microsoft.AspNetCore.Mvc;
using Sherlock.Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Api.Controllers
{
    [Route("api/Push")]
    public class RegisterController : SherlockApiController
    {
        private Lazy<IRegisterService> _registerServiceLazy = null;
        public RegisterController()
        {
            _registerServiceLazy = new Lazy<IRegisterService>(() => WorkContext.Resolve<IRegisterService>());
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost, Route("Register")]
        public object Register(RegisterRequestDto req)
        {
            string retMsg;
            if (req.DeviceTokens.Any(e => e.ChannelId == 0 || string.IsNullOrWhiteSpace(e.DeviceToken)))
            {
                return this.Error(ErrCode.ParameterError, "DeviceTokens中有参数为空");
            }

            if (req.DeviceTokens.Select(e => e.ChannelId).Distinct().Count() < req.DeviceTokens.Count())
            {
                return this.Error(ErrCode.ParameterError, "DeviceTokens中有相同的ChannelId");
            }

            if (string.IsNullOrWhiteSpace(req.DeviceId))
            {
                return this.Error(ErrCode.ParameterError, "DeviceId不能为空");
            }

            var ret = _registerServiceLazy.Value.Register(req, out retMsg);
            if (ret == ErrCode.Sucess || ret == ErrCode.DataIsExist)
                return this.Success(true);
            return this.Error(ret, retMsg);
        }



        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost, Route("Unregister")]
        public object Unregister(UnregisterRequestDto req)
        {
            string retMsg;

            var ret = _registerServiceLazy.Value.Unregister(req, out retMsg);
            if (ret == ErrCode.Sucess)
                return this.Success(true);
            return this.Error(ret, retMsg);
        }




    }
}
