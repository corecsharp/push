using AutoMapper;
using Push.Service.ChannelService.DBModel;
using Push.Service.ChannelService.DomainModel;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.ChannelService.Service
{
    /// <summary>
    /// app走的推送通道
    /// </summary>
    public class PushAppChannelService : IPushAppChannelService
    {
        private IRepository<PushAppChannel> _repository;
        private IMapper _mapper = null;

        public PushAppChannelService(IRepository<PushAppChannel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// 取推送App的通道
        /// </summary>
        /// <param name="appId">app编号</param>
        /// <param name="systemType">系统类型（0：IOS 1:Android）</param>
        /// <param name="channelId">推送通道编号</param>
        /// <returns></returns>
        public async Task<PushAppChannelDomainModel> GetAppChannelByAppInfoAsync(int appId, int systemType, long channelId)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushAppChannel.AppId), appId);
            filter.AddEqual(nameof(PushAppChannel.SystemType), systemType);
            filter.AddEqual(nameof(PushAppChannel.ChannelId), channelId);
            PushAppChannel entity = await _repository.QueryFirstOrDefaultAsync(filter);
            return _mapper.Map<PushAppChannelDomainModel>(entity);
        }
    }
}
