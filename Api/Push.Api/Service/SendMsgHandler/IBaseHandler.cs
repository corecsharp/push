using Push.Api.DTOs;
using Sherlock.Framework;
using System.Collections.Generic;

namespace Push.Api.Service.SendMsgHandler
{
    /// <summary>
    /// 推送消息处理基类
    /// </summary>
    public interface IBaseHandler:IDependency
    {
        /// <summary>
        /// 处理方法
        /// </summary>
        /// <param name="process"></param>
        bool Handle(SendProcessDto process, out string retMsg);

        /// <summary>
        /// 处理方法,批量处理
        /// </summary>
        /// <param name="processList"></param>
        bool Handle(List<SendProcessDto> processList, out string retMsg);
    }
}
