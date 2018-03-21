using Push.Api.DTOs;
using Sherlock.Framework;

namespace Push.Api.Logic
{
    /// <summary>
    /// 推送通道逻辑
    /// </summary>
    public interface IConfigLogic:IDependency
    {
        ConfigDto GetConfig(string config);
        object GetConfigValue(string config);
    }
}
