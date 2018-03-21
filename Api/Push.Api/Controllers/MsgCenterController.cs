using Platform.Core.Filter;
using Push.Api.DTOs;
using Push.Api.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sherlock.Framework.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Push.Service.TokenService.Service;
using AutoMapper;
using System.Threading;
using Sherlock.Framework.Environment;
using Push.Service.MessageCenterService.Service;
using Push.Service.MessageCenterService.DomainModel;
using Sherlock;

namespace Push.Api.Controllers
{
    /// <summary>
    /// 消息中心
    /// </summary>
    [Route("api/MsgCenter")]
    public class MsgCenterController : SherlockApiController
    {
        private Lazy<IPushMessageService> _pushMessageServiceLazy = null;
        private Lazy<ILoggerFactory> _loggerFactoryLazy = null;
        private Lazy<IMapper> _mapperLazy = null;

        public MsgCenterController()
        {
            _pushMessageServiceLazy = new Lazy<IPushMessageService>(() => WorkContext.Resolve<IPushMessageService>());
            _loggerFactoryLazy = new Lazy<ILoggerFactory>(() => WorkContext.Resolve<ILoggerFactory>());
            _mapperLazy = new Lazy<IMapper>(() => WorkContext.Resolve<IMapper>());
        }

        /// <summary>
        /// 获取消息类型列表
        /// </summary>
        /// <param name="msgTypeListRequestDto">消息类型列表请求</param>
        /// <returns></returns>
        [HttpPost, Route("MsgTypeList"), ModelVerify]
        public async Task<object> MsgTypeList(MsgTypeListRequestDto msgTypeListRequestDto)
        {
            IList<PushMessageTypeDomainModel> list = await _pushMessageServiceLazy.Value.GetPushMessageListGroupByMessageTypeAsync(msgTypeListRequestDto.UserId, msgTypeListRequestDto.AppId);
            if (list == null)
            {
                return this.Error(ErrCode.DataIsnotExist, "数据为空");
            }

            return this.Success(list);
        }


        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="msgListRequestDto">消息类型列表请求</param>
        /// <returns></returns>
        [HttpPost, Route("MsgList"), ModelVerify]
        public async Task<object> MsgList(MsgListRequestDto msgListRequestDto)
        {
            IList<PushMessageDomainModel> list = await _pushMessageServiceLazy.Value.GetPushMessageListByMessageTypeAsync(msgListRequestDto.UserId, msgListRequestDto.AppId, msgListRequestDto.MsgType);
            if (list == null)
            {
                return this.Error(ErrCode.DataIsnotExist, "数据为空");
            }
            IList<MsgListResponseDto> ret = _mapperLazy.Value.Map<IList<MsgListResponseDto>>(list);
            return this.Success(ret);
        }


        /// <summary>
        /// 单个消息标识已读
        /// </summary>
        /// <param name="readMsgRequestDto">单个消息已读请求</param>
        /// <returns></returns>
        [HttpPost, Route("ReadMsg"), ModelVerify]
        public async Task<object> ReadMsg(ReadMsgRequestDto readMsgRequestDto)
        {
            int ret = await _pushMessageServiceLazy.Value.ReadPushMessageAsync(readMsgRequestDto.Id, readMsgRequestDto.UserId, readMsgRequestDto.AppId);
            return this.Success($"操作成功，已读{ret}条");
        }


        /// <summary>
        /// 批量消息标识已读
        /// </summary>
        /// <param name="batchReadMsgRequestDto">单个消息已读请求</param>
        /// <returns></returns>
        [HttpPost, Route("BatchReadMsg"), ModelVerify]
        public async Task<object> BatchReadMsg(BatchReadMsgRequestDto batchReadMsgRequestDto)
        {
            int ret = await _pushMessageServiceLazy.Value.BatchSetReadAsync(batchReadMsgRequestDto.Ids, batchReadMsgRequestDto.UserId, batchReadMsgRequestDto.AppId);
            return this.Success($"操作成功，已读{ret}条");
        }

        /// <summary>
        /// 未读消息个数
        /// </summary>
        /// <param name="unReadCountRequestDto">未读消息个数请求</param>
        /// <returns></returns>
        [HttpPost, Route("UnReadCount"), ModelVerify]
        public async Task<object> UnReadCount(UnReadCountRequestDto unReadCountRequestDto)
        {
            int ret = await _pushMessageServiceLazy.Value.UnReadCountAsync(unReadCountRequestDto.UserId, unReadCountRequestDto.AppId);
            return this.Success(ret);
        }

        /// <summary>
        /// 清空消息
        /// </summary>
        /// <param name="unReadCountRequestDto">未读消息个数请求</param>
        /// <returns></returns>
        [HttpPost, Route("ClearMsg"), ModelVerify]
        public async Task<object> ClearMsg(ClearMsgRequestDto clearMsgRequestDto)
        {
            int ret = await _pushMessageServiceLazy.Value.ClearPushMessageAsync(clearMsgRequestDto.UserId, clearMsgRequestDto.AppId);
            return this.Success($"操作成功,清空{ret}条");
        }


        /// <summary>
        /// 清空消息
        /// </summary>
        /// <param name="unReadCountRequestDto">未读消息个数请求</param>
        /// <returns></returns>
        [HttpGet, Route("Test")]
        public async Task<object> Test()
        {
            return this.Success($"Test");
        }

    }
}
