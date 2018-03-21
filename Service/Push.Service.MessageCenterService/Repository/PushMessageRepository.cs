using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using Sherlock.Framework.Data;
using System.Text;
using Push.Service.MessageCenterService.DBModel;
using Platform.Core.Extentions.DapperExtention;
using Push.Service.MessageCenterService.Enum;
using Platform.Core.Helper;

namespace Push.Service.MessageCenterService.Repository
{
    /// <summary>
    /// 消息推送仓储类
    /// </summary>
    public class PushMessageRepository : DapperBaseRepository<PushMessage>, IPushMessageRepository
    {
        /// <summary>
        /// 消息被清空状态
        /// </summary>
        public const int MessageCleardState = 99;

        public PushMessageRepository(DapperContext dapperContext, ILoggerFactory loggerFactory = null) : base(dapperContext, loggerFactory)
        {

        }

        /// <summary>
        /// 批量设置消息阅读状态为已读。
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="Operator"></param>
        /// <returns></returns>
        public async Task<int> BatchSetReadAsync(IEnumerable<long> ids, long userId, int appId)
        {
            if (ids == null || ids.Count() == 0)
            {
                return 0;
            }
            var param = new Dictionary<string, object>();
            var sql = new StringBuilder($"UPDATE push_message " +
                $"                        SET state = @State, update_id=@UserId, update_at=@Now ");
            sql.Append($"WHERE id IN({string.Join(",", ids)}) AND user_id=@UserId AND app_id=@AppId");
            return await this.Context.GetConnection().ExecuteAsync(sql.ToString(),
                new
                {
                    State = (int)ReadState.Read,
                    Now = DateTimeHelper.GetNow(),
                    UserId = userId,
                    AppId = appId
                });
        }

    }
}
