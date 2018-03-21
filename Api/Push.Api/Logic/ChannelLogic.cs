using Push.Service.ChannelService;
using Push.Core.DTOs;
using System.Threading.Tasks;
using Push.Service.ChannelService.Service;
using Push.Api.DTOs;
using AutoMapper;
using Push.Service.ChannelService.DomainModel;

namespace Push.Api.Logic
{
    /// <summary>
    /// 通道相关的逻辑
    /// </summary>
    public class ChannelLogic : IChannelLogic
    {
        private IPushChannelService _pushChannelService;
        private IPushAppChannelService _appChannelService;
        private IMapper _mapper;
        public ChannelLogic(IPushChannelService pushChannelService, IPushAppChannelService appChannelService, IMapper mapper)
        {
            _pushChannelService = pushChannelService;
            _appChannelService = appChannelService;
            _mapper = mapper;
        }

        /// <summary>
        /// 检查通道信息
        /// </summary>
        /// <param name="brandChannelDto"></param>
        /// <param name="channelDto"></param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        public bool CheckChannel(long id, out ChannelDto channelDto, out string retMsg)
        {
            var val = _pushChannelService;
            retMsg = "";
            channelDto = CourseCacheLogic<string, ChannelDto>.Get("Channel_" + id,
                 () =>
               {
                   PushChannelDomainModel pushChannel = _pushChannelService.GetChannelByIdAsync(id).Result;
                   return _mapper.Map<ChannelDto>(pushChannel);
               });
            if (channelDto == null)
            {
                retMsg = string.Format("ChannelId:{0},未匹配到Push_Channel", id);
                return false;
            }
            //通道是否有效
            if (!channelDto.IsActive)
            {
                retMsg = string.Format("ChannelId:{0},通道{1}状态无效", channelDto.Id, channelDto.ChannelName);
                return false;
            }
            return true;
        }


        /// <summary>
        /// 检查AppChannel
        /// </summary>
        /// <param name="rzTokenBrandDto"></param>
        /// <param name="channelDto"></param>
        /// <param name="appChannelDto"></param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        public bool CheckAppChannel(int appId, int systemType, long channelId, out AppChannelDto appChannelDto, out string retMsg)
        {
            retMsg = "";
            appChannelDto = CourseCacheLogic<string, AppChannelDto>.Get(string.Format("AppChannel_AppId_{0}_SystemType_{1}_ChannelId_{2}", appId, systemType, channelId),
               () =>
               {
                   PushAppChannelDomainModel domainModel = _appChannelService.GetAppChannelByAppInfoAsync(appId, systemType, channelId).Result;
                   return _mapper.Map<AppChannelDto>(domainModel);
               });
            if (appChannelDto == null)
            {
                retMsg = string.Format("通过AppId:{0}、SystemType:{1}、ChannelId:{2},未匹配到Push_App_Channel", appId, systemType, channelId);
                return false;
            }
            return true;
        }




    }
}
