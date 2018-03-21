using Push.Service.TokenService.DBModel;
using Push.Service.TokenService.DomainModel;
using Sherlock.Framework;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.TokenService.Repository
{
    /// <summary>
    /// 手机设备注册
    /// </summary>
    public interface IPushTokenBrandRepository : IRepository<PushTokenBrand>, IDependency
    {
        /// <summary>
        /// 通过消息的Token和AppId获取设备通道信息（ChannelId, URL, SystemType, AppKey, AppSecret）
        /// </summary>
        /// <param name="token"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        Task<List<DeviceChannelDomainModel>> GetDeviceChannelListAsync(string token, long appId);

        /// <summary>
        /// 通过消息的Token和AppId单个获取设备通道信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DeviceChannelDomainModel> GetDeviceChannelListByTokenBrandIdAsync(long id);

        /// <summary>
        /// 尝试清空在该设备上注册的以前的用户
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<int> TryClearDeviceOldUser(string deviceId, string userId);
    }
}
