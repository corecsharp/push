using Push.Service.ProcessService.DomainModel;
using Sherlock.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.ProcessService.Service
{
    /// <summary>
    /// 待推送的消息，如果一个token对应多个手机，则一条消息拆成多条去发，如果发送失败，回写待发送消息，记录哪个手机设备发送失败。
    /// </summary>
    public interface IPushSendProcessService : IDependency
    {
        /// <summary>
        /// 插入待发送列表
        /// </summary>
        /// <param name="domainModel"></param>
        /// <returns></returns>
        Task<int> InsertProcessAsync(PushSendProcessDomainModel domainModel);

        /// <summary>
        /// 根据Id获取待推送消息
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        Task<PushSendProcessDomainModel> GetProcessByIdAsync(long processId);

        /// <summary>
        /// 更新该条Process的使用状态，保证同一条数据不被重复处理
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        Task<int> UpdateProcessUseStatusByIdAsync(long processId, DateTime expireTime);

        /// <summary>
        /// 根据id删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteProcessByIdAsync(long id);

        /// <summary>
        /// 回写ProcessId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sendTime"></param>
        /// <returns></returns>
        Task<int> WriteBackProcessByIdAsync(long id, DateTime sendTime);

        /// <summary>
        /// 更新待取出Id的批号
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        Task<int> UpdateBatchNOAsync(Guid guid, DateTime expireTime);

        /// <summary>
        /// 根据批号取该批次的Id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        Task<List<long>> SelectProcessIdListByBatchNOAsync(Guid guid);

        /// <summary>
        /// 批量新增推送消息
        /// </summary>
        /// <param name="pushList"></param>
        /// <returns></returns>
        Task<int> InsertProcessListAsync(List<PushSendProcessDomainModel> pushList);

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

        /// <summary>
        /// 根据批次获取待发送的消息
        /// </summary>
        /// <param name="batchNO"></param>
        /// <returns></returns>
        Task<List<PushSendProcessDomainModel>> GetProcessListByBatchNOAsync(string batchNO);

        /// <summary>
        /// 通过Id集合删除数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> DeleteProcessByIdsAsync(List<long> ids);

        Task<int> WriteBackProcessByIdsAsync(List<long> ids, DateTime sendTime);

        /// <summary>
        /// 获取待发送总数
        /// </summary>
        /// <returns></returns>
        Task<int> GetSendProcessCountAsync();
    }
}
