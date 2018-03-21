using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sherlock.Framework;

namespace Platform.Core.Filter
{
    public class ErrorFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger logger;
        public ErrorFilterAttribute(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<ErrorFilterAttribute>();
        }
        public override void OnException(ExceptionContext context)
        {
            //context.
            var action = context.ActionDescriptor.DisplayName;
            logger.WriteError($"{action}发生错误", context.Exception);
            context.Result = new ObjectResult(null) { StatusCode = 500 };
            base.OnException(context);
        }
    }
}
