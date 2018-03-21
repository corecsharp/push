using Sherlock.Framework;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Push.Service.ConfigService.DBModel;
using Push.Service.ConfigService.DomainModel;

namespace Push.Service.ConfigService
{
    /// <summary>
    /// 推送配置
    /// </summary>
    public interface IPushConfigService : IDependency
    {
        /// <summary>
        /// 通过key获取配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<PushConfigDomainModel> GetConfigByKeyAsync(string key);
    }
}