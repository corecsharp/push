using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Push.Service.ConfigService.DBModel;
using Sherlock.Framework;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Push.Service.ConfigService.DomainModel;

namespace Push.Service.ConfigService
{
    /// <summary>
    /// 推送配置
    /// </summary>
    public class PushConfigService : IPushConfigService
    {
        private IRepository<PushConfig> _repository;
        private IMapper _mapper = null;


        public PushConfigService(IMapper mapper, IRepository<PushConfig> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// 通过key获取配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<PushConfigDomainModel> GetConfigByKeyAsync(string key)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushConfig.ConfigKey), key);
            PushConfig entity = await _repository.QueryFirstOrDefaultAsync(filter);
            return _mapper.Map<PushConfigDomainModel>(entity);
        }
    }
}