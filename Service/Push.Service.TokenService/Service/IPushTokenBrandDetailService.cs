using Sherlock.Framework;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Push.Service.TokenService.DBModel;
using Push.Service.TokenService.DomainModel;

namespace Push.Service.TokenService.Service
{
    /// <summary>
    /// 手机设备注册明细，包括几个推送平台的token
    /// </summary>
    public interface IPushTokenBrandDetailService : IDependency
    {
        /// <summary>
        /// 获取设备的Token
        /// </summary>
        /// <param name="tokenBrandId"></param>
        /// <param name="channelId"></param>
        /// <returns></returns>
        Task<PushTokenBrandDetail> GetDeviceTokenAsync(long tokenBrandId, long channelId);

        /// <summary>
        /// 通过TokenBrandId获取明细列表
        /// </summary>
        /// <param name="tokenBrandId"></param>
        /// <returns></returns>
        Task<List<PushTokenBrandDetail>> GetDeviceTokenListAsync(long tokenBrandId);

        /// <summary>
        /// 插入明细
        /// </summary>
        /// <param name="domainModel"></param>
        /// <returns></returns>
        Task<int> InsertAsync(PushTokenBrandDetailDomainModel domainModel);

        /// <summary>
        /// 通过TokenBrandId删除注册明细
        /// </summary>
        /// <param name="tokenBrandId"></param>
        /// <returns></returns>
        Task<int> DeleteByTokenBrandIdAsync(long tokenBrandId);

    }
}