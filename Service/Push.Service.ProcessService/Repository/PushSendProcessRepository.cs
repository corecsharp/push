using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Push.Service.ProcessService.DBModel;
using Sherlock.Framework;
using Microsoft.Extensions.Logging;
using Dapper;
using Platform.Core.Helper;
using Push.Service.ProcessService.DomainModel;

namespace Push.Service.ProcessService.Repository
{
    /// <summary>
    /// 待推送的消息，如果一个token对应多个手机，则一条消息拆成多条去发，如果发送失败，回写待发送消息，记录哪个手机设备发送失败。
    /// </summary>
    public class PushSendProcessRepository : DapperRepository<PushSendProcess>, IPushSendProcessRepository
    {
        public PushSendProcessRepository(DapperContext dapperContext, ILoggerFactory loggerFactory = null) : base(dapperContext, loggerFactory)
        {

        }



        /// <summary>
        /// 获取appId和channleId
        /// </summary>
        /// <param name="timeNow"></param>
        /// <returns></returns>
        public async Task<PushSendProcessAppChannelDomainModel> GetAppIdAndChannelIdAsync(DateTime timeNow)
        {
            string sql = "SELECT app_id,channel_id "
                       + "FROM push_send_process "
                       + "WHERE start_time<=@Now AND (batch_no IS NULL OR batch_expire_time<=@Now) AND brand_id IS NOT NULL AND channel_id IS NOT NULL AND device_token IS NOT NULL "
                       + "ORDER BY CASE delay_times WHEN 0 THEN 100 ELSE delay_times END DESC,priority_level,start_time "
                       + "LIMIT 1 ";

            var param = new Dictionary<string, object>()
            {
                {"Now", timeNow }
            };

            return await Context.GetConnection().QueryFirstOrDefaultAsync<PushSendProcessAppChannelDomainModel>(sql, param);
        }






        /// <summary>
        /// 更新批次号
        /// </summary>
        /// <param name="batchNo"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public async Task<int> UpdateBatchNOAsync(string batchNo, DateTime expireTime)
        {
            var sql = "UPDATE push_send_process SET batch_no=@Guid,batch_expire_time=@ExpireTime "
                    + "WHERE push_send_process.id IN( "
                    + "SELECT id FROM ( "
                    + "SELECT id, (CASE delay_times WHEN 0 THEN 100 ELSE delay_times END) DT "
                    + "FROM push_send_process "
                    + "WHERE start_time<=@NOW AND (is_used=0 OR expire_time<@NOW) AND (batch_no IS NULL OR batch_expire_time<=@NOW) "
                    + "ORDER BY DT DESC,priority_level,start_time) T)";
            var param = new Dictionary<string, object>()
            {
                {"Guid",batchNo},
                {"ExpireTime",expireTime},
                {"NOW",DateTimeHelper.GetNow()}
            };

            return await Context.GetConnection().ExecuteAsync(sql, param);
        }

        /// <summary>
        /// 通过appId和channelId更新批次号
        /// </summary>
        /// <param name="domainModel"></param>
        /// <returns></returns>
        public async Task<int> UpdateBatchNOByAppIdAndChannelIdAsync(BatchProcessParmsDomainModel domainModel)
        {

            var sql = "UPDATE push_send_process SET batch_no=@BatchNO,batch_expire_time=@ExpireTime "
                    + "WHERE push_send_process.id IN ( "
                    + "SELECT id FROM ( "
                    + "SELECT id, (CASE delay_times WHEN 0 THEN 100 ELSE delay_times END) DT "
                    + "FROM push_send_process "
                    + "WHERE start_time<=@TimeNow AND (batch_no IS NULL OR batch_expire_time<=@TimeNow) AND app_id=@AppId AND channel_id=@ChannelId "
                    + "ORDER BY DT DESC,priority_level,start_time "
                    + "LIMIT @TopNum ) T ) "
                    + "AND brand_id IS NOT NULL AND channel_id IS NOT NULL AND device_token IS NOT NULL ";

            return await Context.GetConnection().ExecuteAsync(sql, domainModel);

        }

        /// <summary>
        /// 更新使用状态
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public async Task<int> UpdateProcessUseStatusByIdAsync(long processId, DateTime expireTime)
        {
            var sql = "UPDATE push_send_process "
                    + "SET is_used=1,expire_time=@ExpireTime "
                    + "WHERE Id=@Id AND (is_used=0 or expire_time<@Now) ";

            var param = new Dictionary<string, object>()
            {
                {"Id",processId },
                {"ExpireTime",expireTime },
                {"Now",DateTimeHelper.GetNow() }
            };

            return await Context.GetConnection().ExecuteAsync(sql, param);
        }

        /// <summary>
        /// 通过Id回写待发送信息的状态信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sendTime"></param>
        /// <returns></returns>
        public async Task<int> WriteBackProcessByIdAsync(long id, DateTime sendTime)
        {
            var sql = "UPDATE push_send_process "
                    + "SET is_used=0,batch_no=NULL,send_time=@SendTime,delay_times=delay_times+1 "
                    + "WHERE id=@Id ";

            var param = new Dictionary<string, object>()
            {
                {"SendTime",sendTime },
                {"Id",id }
            };

            return await Context.GetConnection().ExecuteAsync(sql, param);
        }

        /// <summary>
        /// 通过Ids回写待发送信息的状态信息
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="sendTime"></param>
        /// <returns></returns>
        public async Task<int> WriteBackProcessByIdsAsync(List<long> ids, DateTime sendTime)
        {
            if (ids == null || ids.Count == 0)
            {
                return 0;
            }

            var sql = "UPDATE push_send_process "
                    + "SET is_used=0,batch_no=NULL,send_time=@SendTime,delay_times=delay_times+1 "
                    + $"WHERE id IN ({string.Join(",", ids)})";

            var param = new Dictionary<string, object>()
            {
                {"SendTime",sendTime }
            };

            return await Context.GetConnection().ExecuteAsync(sql, param);
        }
    }
}