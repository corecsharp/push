using Platform.Core.Extentions;
using System.Net;
using System.Net.Http;
using Platform.Core;
using System.Collections.Generic;
using Platform.Core.Entity.Response;
using Microsoft.AspNetCore.Mvc;

namespace Sherlock.Framework.Web.Mvc
{
    public static class SherlockApiControllerExtentions
    {

        public static object Error<T>(this SherlockApiController controller, T enumCode)
            where T : struct
        {
            return Error(controller, enumCode, default(object));
        }

        public static object Error<T, TObj>(this SherlockApiController controller, T enumCode, TObj obj)
            where T : struct
            where TObj : class
        {
            //var response = obj.IncludeResponse();
            var def = enumCode.GetErrDefinition();
            return Error(controller, obj, def.Code, def.Msg, HttpStatusCode.BadRequest);
        }

        public static object Error<T>(this SherlockApiController controller, T enumCode, string message)
                where T : struct

        {
            var def = enumCode.GetErrDefinition();
            return Error(controller, default(object), def.Code, message, HttpStatusCode.BadRequest);
        }

        static object Error<T>(this SherlockApiController controller, T obj, string code, string message, HttpStatusCode httpCode)
        {
            var response = obj.IncludeResponse(message, code);
            controller.SetStatusCode(httpCode);
            //var res = controller.StatusCode((int)httpCode, response);
            return response;
        }

        public static object Success(this SherlockApiController controller)
        {
            controller.SetStatusCode(HttpStatusCode.OK);
            return default(object);
        }

        public static object Success<T>(this SherlockApiController controller, T obj)
        {
            var response = obj.IncludeResponse();
            controller.SetStatusCode(HttpStatusCode.OK);
            return response;
        }

        /// <summary>
        /// 拒绝请求，因为无相关权限，返回403
        /// </summary>
        /// <returns></returns>
        public static object Deny(this SherlockApiController controller)
        {
            var detail = ErrCodeCommon.PermissionDenied.GetErrDefinition();
            return Error(controller, default(object), detail.Code, detail.Msg, HttpStatusCode.Forbidden);
        }

        static void SetStatusCode(this SherlockApiController controller, HttpStatusCode code)
        {
            controller.Response.StatusCode = (int)code;
        }
    }
}
