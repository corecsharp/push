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
    /// 消息类型列表请求验证
    /// </summary>
    public class MsgTypeListRequestValidator : DependencyValidator<MsgTypeListRequestDto>
    {
        public MsgTypeListRequestValidator()
        {
            this.RuleFor(s => s.UserId).NotNull().WithMessage($"{nameof(MsgTypeListRequestDto.UserId)}不能为空")
                .GreaterThan(0).WithMessage($"{nameof(MsgTypeListRequestDto.UserId)}参数不合法");
                
            this.RuleFor(s => s.AppId).NotNull().WithMessage($"{nameof(MsgTypeListRequestDto.AppId)}不能为空")
                .GreaterThan(0).WithMessage($"{nameof(MsgTypeListRequestDto.AppId)}参数不合法");
        }
    }

    /// <summary>
    /// 消息列表请求验证
    /// </summary>
    public class MsgListRequestValidator : DependencyValidator<MsgListRequestDto>
    {
        public MsgListRequestValidator()
        {
            this.RuleFor(s => s.UserId).NotNull().WithMessage($"{nameof(MsgListRequestDto.UserId)}不能为空")
                .GreaterThan(0).WithMessage($"{nameof(MsgListRequestDto.UserId)}参数不合法");

            this.RuleFor(s => s.AppId).NotNull().WithMessage($"{nameof(MsgListRequestDto.AppId)}不能为空")
                .GreaterThan(0).WithMessage($"{nameof(MsgListRequestDto.AppId)}参数不合法");
        }
    }

    /// <summary>
    /// 单个消息标识请求验证
    /// </summary>
    public class ReadMsgRequestValidator : DependencyValidator<ReadMsgRequestDto>
    {
        public ReadMsgRequestValidator()
        {
            this.RuleFor(s => s.Id).NotNull().WithMessage($"{nameof(ReadMsgRequestDto.Id)}不能为空")
                .GreaterThan(0).WithMessage($"{nameof(ReadMsgRequestDto.Id)}参数不合法");

            this.RuleFor(s => s.UserId).NotNull().WithMessage($"{nameof(ReadMsgRequestDto.UserId)}不能为空")
                .GreaterThan(0).WithMessage($"{nameof(ReadMsgRequestDto.UserId)}参数不合法");

            this.RuleFor(s => s.AppId).NotNull().WithMessage($"{nameof(ReadMsgRequestDto.AppId)}不能为空")
                .GreaterThan(0).WithMessage($"{nameof(ReadMsgRequestDto.AppId)}参数不合法");
        }
    }

    /// <summary>
    /// 批量消息标识请求验证
    /// </summary>
    public class BatchReadMsgRequestValidator : DependencyValidator<BatchReadMsgRequestDto>
    {
        public BatchReadMsgRequestValidator()
        {
            this.RuleFor(s => s.Ids).NotNull().WithMessage($"{nameof(BatchReadMsgRequestDto.Ids)}不能为空")
            .Must((s) => { return s.Length > 0; }).WithMessage($"{nameof(BatchReadMsgRequestDto.Ids)}个数必须大于0");

            this.RuleFor(s => s.UserId).NotNull().WithMessage($"{nameof(BatchReadMsgRequestDto.UserId)}不能为空")
                .GreaterThan(0).WithMessage($"{nameof(BatchReadMsgRequestDto.UserId)}参数不合法");

            this.RuleFor(s => s.AppId).NotNull().WithMessage($"{nameof(BatchReadMsgRequestDto.AppId)}不能为空")
                .GreaterThan(0).WithMessage($"{nameof(BatchReadMsgRequestDto.AppId)}参数不合法");
        }
    }

    /// <summary>
    /// 未读消息个数请求验证
    /// </summary>
    public class UnReadCountRequestValidator : DependencyValidator<UnReadCountRequestDto>
    {
        public UnReadCountRequestValidator()
        {
            this.RuleFor(s => s.UserId).NotNull().WithMessage($"{nameof(UnReadCountRequestDto.UserId)}不能为空")
                .GreaterThan(0).WithMessage($"{nameof(UnReadCountRequestDto.UserId)}参数不合法");

            this.RuleFor(s => s.AppId).NotNull().WithMessage($"{nameof(UnReadCountRequestDto.AppId)}不能为空")
                .GreaterThan(0).WithMessage($"{nameof(UnReadCountRequestDto.AppId)}参数不合法");
        }
    }

    /// <summary>
    /// 未读消息个数请求验证
    /// </summary>
    public class ClearMsgRequestValidator : DependencyValidator<ClearMsgRequestDto>
    {
        public ClearMsgRequestValidator()
        {
            this.RuleFor(s => s.UserId).NotNull().WithMessage($"{nameof(ClearMsgRequestDto.UserId)}不能为空")
                .GreaterThan(0).WithMessage($"{nameof(ClearMsgRequestDto.UserId)}参数不合法");

            this.RuleFor(s => s.AppId).NotNull().WithMessage($"{nameof(ClearMsgRequestDto.AppId)}不能为空")
                .GreaterThan(0).WithMessage($"{nameof(ClearMsgRequestDto.AppId)}参数不合法");
        }
    }

}
