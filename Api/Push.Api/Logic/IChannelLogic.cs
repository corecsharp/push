using System.Threading.Tasks;
using Sherlock.Framework;
using Push.Api.DTOs;

namespace Push.Api.Logic
{
    /// <summary>
    /// 通道相关的逻辑
    /// </summary>
    public interface IChannelLogic : IDependency
    {
        /// <summary>
        /// 检查通道信息
        /// </summary>
        /// <param name="brandChannelDto"></param>
        /// <param name="channelDto"></param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        bool CheckChannel(long id, out ChannelDto channelDto, out string retMsg);
        /// <summary>
        /// 检查AppChannel
        /// </summary>
        /// <param name="rzTokenBrandDto"></param>
        /// <param name="channelDto"></param>
        /// <param name="appChannelDto"></param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        bool CheckAppChannel(int appId, int systemType, long channelId, out AppChannelDto appChannelDto, out string retMsg);
    }
}
