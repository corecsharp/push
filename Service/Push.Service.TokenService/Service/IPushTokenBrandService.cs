using Push.Service.TokenService.DomainModel;
using Sherlock.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.TokenService.Service
{
    /// <summary>
    ///  手机设备注册
    /// </summary>
    public interface IPushTokenBrandService : IDependency
    {
        /// <summary>
        /// 获取通过Token获取注册列表
        /// </summary>
        /// <param name="RzToken"></param>
        /// <returns></returns>
        Task<List<PushTokenBrandDomainModel>> GetTokenBrandListByTokenAsync(string token);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="domainModel"></param>
        /// <returns></returns>
        Task<int> InsertTokenBrandAsync(RegisterRequestDomainModel domainModel);

        /// <summary>
        /// 根据id删除TokenBrand
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteTokenBrandByIdAsync(long id);

        /// <summary>
        /// 通过Id获取TokenBrand
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PushTokenBrandDomainModel> GetTokenBrandByIdAsync(long id);

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
        Task<int> TryClearDeviceOldUserAsync(string deviceId, string userId);

        Task<List<string>> GetAllTokenBrand(int? appId);

    }
}
