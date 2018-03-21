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
    /// 推送通道
    /// </summary>
    public class PushChannelService : IPushChannelService
    {
        private IRepository<PushChannel> _repository;
        private IMapper _mapper = null;

        public PushChannelService(IRepository<PushChannel> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// 根据id获取推送通道
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PushChannelDomainModel> GetChannelByIdAsync(long id)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushChannel.Id), id);
            var entity = await _repository.QueryFirstOrDefaultAsync(filter);
            return _mapper.Map<PushChannelDomainModel>(entity);
        }
    }
}
