using Push.Api.DTOs;
using Sherlock.Framework.Web.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Push.Api.Validators
{
    /// <summary>
    /// 接受信息验证
    /// </summary>
    public class SendMsgRequestValidator : DependencyValidator<SendMsgRequest>
    {
        public SendMsgRequestValidator()
        {
            this.RuleFor(s => s.Token).NotEmpty().WithMessage($"{nameof(SendMsgRequest.Token)}不能为空")
                .MinimumLength(1).WithMessage($"{nameof(SendMsgRequest.Token)}长度至少为1")
                .MaximumLength(128).WithMessage($"{nameof(SendMsgRequest.Token)}长度不能超过128");

            this.RuleFor(s => s.AppId).NotNull().WithMessage($"{nameof(SendMsgRequest.AppId)}不能为空");

            this.RuleFor(s => s.Msg).NotEmpty().WithMessage($"{nameof(SendMsgRequest.Msg)}不能为空")
                .MinimumLength(1).WithMessage($"{nameof(SendMsgRequest.Msg)}长度至少为1")
                .MaximumLength(1000).WithMessage($"{nameof(SendMsgRequest.Msg)}长度不能超过1000");

            this.RuleFor(s => s.Title).NotEmpty().WithMessage($"{nameof(SendMsgRequest.Title)}不能为空")
                .MinimumLength(1).WithMessage($"{nameof(SendMsgRequest.Title)}长度至少为1")
                .MaximumLength(100).WithMessage($"{nameof(SendMsgRequest.Title)}长度不能超过100");
        }
    }

    public class SendMsgListRequestValidator : DependencyValidator<SendMsgListRequest>
    {
        public SendMsgListRequestValidator()
        {
            this.RuleFor(s => s.TokenList).NotNull().WithMessage($"{nameof(SendMsgListRequest.TokenList)}不能为空");

            this.RuleFor(s => s.AppId).NotNull().WithMessage($"{nameof(SendMsgListRequest.AppId)}不能为空");

            this.RuleFor(s => s.Msg).NotEmpty().WithMessage($"{nameof(SendMsgListRequest.Msg)}不能为空")
                .MinimumLength(1).WithMessage($"{nameof(SendMsgListRequest.Msg)}长度至少为1")
                .MaximumLength(1000).WithMessage($"{nameof(SendMsgListRequest.Msg)}长度不能超过1000");

            this.RuleFor(s => s.Title).NotEmpty().WithMessage($"{nameof(SendMsgListRequest.Title)}不能为空")
                .MinimumLength(1).WithMessage($"{nameof(SendMsgListRequest.Title)}长度至少为1")
                .MaximumLength(100).WithMessage($"{nameof(SendMsgListRequest.Title)}长度不能超过100");
        }
    }

    public class SendMsgDtoValidator : DependencyValidator<SendMsgDto>
    {
        public SendMsgDtoValidator()
        {
            this.RuleFor(s => s.AppId).NotNull().WithMessage($"{nameof(SendMsgDto.AppId)}不能为空");

            this.RuleFor(s => s.Msg).NotEmpty().WithMessage($"{nameof(SendMsgDto.Msg)}不能为空")
                .MinimumLength(1).WithMessage($"{nameof(SendMsgDto.Msg)}长度至少为1")
                .MaximumLength(1000).WithMessage($"{nameof(SendMsgDto.Msg)}长度不能超过1000");

            this.RuleFor(s => s.Title).NotEmpty().WithMessage($"{nameof(SendMsgDto.Title)}不能为空")
                .MinimumLength(1).WithMessage($"{nameof(SendMsgDto.Title)}长度至少为1")
                .MaximumLength(100).WithMessage($"{nameof(SendMsgDto.Title)}长度不能超过100");
        }
    }
}
