using Platform.Core.Extentions;
using Sherlock.Framework.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sherlock.Framework.Web.Mvc
{
    public static class WorkContextExtention
    {
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static UserSession GetCurrentUser(this WorkContext context)
        {
            return context.CurrentUser as UserSession;
        }
    }
}
