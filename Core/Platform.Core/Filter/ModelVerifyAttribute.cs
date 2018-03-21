using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Platform.Core.Entity.Response;
using Platform.Core.Extentions;

namespace Platform.Core.Filter
{
    public class ModelVerifyAttribute : ActionFilterAttribute
    {
        public bool AllowNullParameter { get; set; }
        public bool AllowEmptyParameter { get; set; }

        public ModelVerifyAttribute()
        {
            //优先级高于AuthAttribute，在参数不正确的情况下可以减少一次redis请求
            Order = -1;
        }

        private IActionResult GetValidateResult(ActionExecutingContext context)
        {
            if (!AllowNullParameter)
            {
                foreach (var item in context.ActionArguments.Where(p => p.Value == null
                                                    || (!AllowEmptyParameter && Equals(p.Value, p.Value.GetType().DefaultValue()))))
                {
                    context.ModelState.AddModelError(item.Key, $"{item.Key}不可为空");
                }
            }
            if (!context.ModelState.IsValid)
            {
                var errors = new Dictionary<string, string>();
                foreach (var keyValue in context.ModelState)
                {
                    if (keyValue.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    {
                        errors[keyValue.Key] = keyValue.Value.Errors.Select(
                                        e => string.IsNullOrWhiteSpace(e.ErrorMessage)
                                            ? $"参数{keyValue.Key}的值不合法"
                                            : e.ErrorMessage).FirstOrDefault();
                    }
                }
                var errmsg = errors.Select(e => e.Value).FirstOrDefault();
                var errdetail = ErrCodeCommon.ParameterError.GetErrDefinition();
                var response = default(object).IncludeResponse($"{errdetail.Msg}:{errmsg}", errdetail.Code);
                return new ObjectResult(response) { StatusCode = 400 };
            }
            return null;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //DONE:验证失败，返回错误信息
            context.Result = GetValidateResult(context);
            base.OnActionExecuting(context);
        }
    }
}
