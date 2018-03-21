using Platform.Core.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sherlock.Framework.Environment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Core.Filter
{
    public class ActionExcuteFilterAttribute : ActionFilterAttribute
    {
        public Guid GuidKey { get; set; }
        public string ActionKey { get; set; } = "action";
        public string RenderKey { get; set; } = "render";
        public int ApplicationLogTimeSpan { get; set; }

        private Lazy<ILogger> _logger = null;      //延迟加载
        private IOptionsSnapshot<ActionLogOptions> _logOptions = null;
        public ActionExcuteFilterAttribute()
        {
            _logger = new Lazy<ILogger>(() => SherlockEngine.Current.GetService<ILoggerFactory>().CreateLogger("ActionLog"));
            _logOptions = new Lazy<IOptionsSnapshot<ActionLogOptions>>(() => SherlockEngine.Current.GetService<IOptionsSnapshot<ActionLogOptions>>()).Value;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (_logOptions.Value.IsOpen)
                GetTimer(filterContext, ActionKey).Start();
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (_logOptions.Value.IsOpen)
                GetTimer(filterContext, ActionKey).Stop();
            base.OnActionExecuted(filterContext);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (_logOptions.Value.IsOpen)
            {
                ApplicationLogTimeSpan = _logOptions.Value.ApplicationLogTimeSpan;
                var renderTimer = GetTimer(filterContext, RenderKey);
                renderTimer.Stop();

                var actionTimer = GetTimer(filterContext, ActionKey);

                if (actionTimer.ElapsedMilliseconds >= ApplicationLogTimeSpan || renderTimer.ElapsedMilliseconds >= ApplicationLogTimeSpan)
                {
                    _logger.Value.LogInformation(String.Format(
                            "ActionLog_{0}【{1}】-【{2}】,执行:{3}ms,渲染:{4}ms",
                            DateTime.UtcNow,
                            filterContext.RouteData.Values["controller"],
                            filterContext.RouteData.Values["action"],
                            actionTimer.ElapsedMilliseconds,
                            renderTimer.ElapsedMilliseconds));
                }
            }
            base.OnResultExecuted(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (_logOptions.Value.IsOpen)
                GetTimer(filterContext, RenderKey).Start();
            base.OnResultExecuting(filterContext);
        }
        private Stopwatch GetTimer(ActionContext context, string name)
        {
            string key = "__timer__" + name;
            if (context.HttpContext.Items.Keys.Contains(key))
            {
                return (Stopwatch)context.HttpContext.Items[key];
            }
            var result = new Stopwatch();
            context.HttpContext.Items[key] = result;
            return result;
        }
    }
}
