using Platform.Core.Extentions;
using System.Net;
using System.Net.Http;
using Platform.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sherlock.Framework.Web.Mvc
{
    /// <summary>
    /// 暂时使用SherlockController，因为路由中的参数不能映射上
    /// </summary>
    public static class SherlockControllerExtentions
    {

        public static IActionResult Error<T>(this SherlockController controller, T enumCode)
            where T : struct
        {
            return Error(controller, enumCode, default(object));
        }

        public static IActionResult Error<T, TObj>(this SherlockController controller, T enumCode, TObj obj)
            where T : struct
            where TObj : class
        {
            //var response = obj.IncludeResponse();
            var def = enumCode.GetErrDefinition();
            return Error(controller, obj, def.Code, def.Msg, HttpStatusCode.BadRequest);
        }

        static IActionResult Error<T>(this SherlockController controller, T obj, string code, string message, HttpStatusCode httpCode)
        {
            var response = obj.IncludeResponse(message, code);
            return controller.StatusCode((int)httpCode, response);
        }

        public static IActionResult Success(this SherlockController controller)
        {
            return controller.StatusCode((int)HttpStatusCode.OK);
        }

        public static IActionResult Success<T>(this SherlockController controller, T obj)
        {
            var response = obj.IncludeResponse();
            return controller.StatusCode((int)HttpStatusCode.OK, response);
        }

        /// <summary>
        /// 拒绝请求，因为无相关权限，返回403
        /// </summary>
        /// <returns></returns>
        public static IActionResult Deny(this SherlockController controller)
        {
            var detail = ErrCodeCommon.PermissionDenied.GetErrDefinition();
            return Error(controller, default(object), detail.Code, detail.Msg, HttpStatusCode.Forbidden);
        }

    }
}
