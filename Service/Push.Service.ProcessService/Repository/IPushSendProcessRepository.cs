using Sherlock.Framework;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Push.Service.ProcessService.DBModel;
using Push.Service.ProcessService.DomainModel;

namespace Push.Service.ProcessService.Repository
{
    /// <summary>
    /// 待推送的消息，如果一个token对应多个手机，则一条消息拆成多条去发，如果发送失败，回写待发送消息，记录哪个手机设备发送失败。
    /// </summary>
    public interface IPushSendProcessRepository: IRepository<PushSendProcess>, IDependency
    {
        /// <summary>
        /// 更新该条Process的使用状态，保证同一条数据不被重复处理
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        Task<int> UpdateProcessUseStatusByIdAsync(long processId, DateTime expireTime);
        Task<int> WriteBackProcessByIdAsync(long id, DateTime sendTime);

        /// <summary>
        /// 更新待取出Id的批号
        /// </summary>
        /// <param name="batchNo"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        Task<int> UpdateBatchNOAsync(string batchNo, DateTime expireTime);

        /// <summary>
        /// 按规则排序，获取第一条的应用和通道信息，以被批量获取要处理的信息
        /// </summary>
        /// <param name="timeNow"></param>
        /// <returns></returns>
        Task<PushSendProcessAppChannelDomainModel> GetAppIdAndChannelIdAsync(DateTime timeNow);

        /// <summary>
        /// 通过应用和通道，批量更新数据
        /// </summary>
        /// <param name="domainModel"></param>
        /// <returns></returns>
        Task<int> UpdateBatchNOByAppIdAndChannelIdAsync(BatchProcessParmsDomainModel domainModel);

        Task<int> WriteBackProcessByIdsAsync(List<long> ids, DateTime sendTime);
    }
}