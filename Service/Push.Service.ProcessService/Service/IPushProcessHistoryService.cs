using Sherlock.Framework;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Push.Service.ProcessService.DBModel;
using Push.Service.ProcessService.DomainModel;

namespace Push.Service.ProcessService.Service
{
    /// <summary>
    /// 推送消息历史发送记录。记录，无论成功或失败。
    /// </summary>
    public interface IPushProcessHistoryService :  IDependency
    {
        /// <summary>
        /// 新增推送消息
        /// </summary>
        /// <returns></returns>
        Task<int> InsertProcessHistoryAsync(AddProcessHistoryDomainModel model);

        /// <summary>
        /// 批量新增推送消息
        /// 使用事务，不能插入数据
        /// </summary>
        /// <param name="historyList"></param>
        /// <returns></returns>
        Task<int> InsertProcessHistoryListAsync(List<AddProcessHistoryDomainModel> historyList);

        /// <summary>
        /// 获取一段时间内，发送失败总数
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        Task<int> GetSendFailureCountAsync(DateTime beginTime, DateTime endTime);
    }
}