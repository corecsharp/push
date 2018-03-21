using AutoMapper;
using Push.Service.ConfigService.DBModel;
using Push.Service.ConfigService.DomainModel;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.ConfigService.Service
{
    /// <summary>
    /// 字典
    /// </summary>
    public class InfraDicService : IInfraDicService
    {
        private IRepository<InfraDic> _repository;
        private IMapper _mapper = null;

        public InfraDicService(IRepository<InfraDic> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        /// <summary>
        /// 根据字典类型获取字典的列表
        /// </summary>
        /// <param name="dicType"></param>
        /// <returns></returns>
        public async Task<InfraDicDomainModel> GetDicByTypeAndKeyAsync(string dicType, string dicKey)
        {
            var filter = new SingleQueryFilter();
            if (!string.IsNullOrWhiteSpace(dicType))
            {
                filter.AddEqual(nameof(InfraDic.Type), dicType);
            }
            if (!string.IsNullOrWhiteSpace(dicKey))
            {
                filter.AddEqual(nameof(InfraDic.Key), dicKey);
            }
            InfraDic infraDic = await _repository.QueryFirstOrDefaultAsync(filter.IsEmpty ? null : filter);
            return _mapper.Map<InfraDicDomainModel>(infraDic);
        }
        /// <summary>
        /// 根据类型和键值获取字典
        /// </summary>
        /// <param name="dicType"></param>
        /// <param name="dicValue"></param>
        /// <returns></returns>
        public async Task<InfraDicDomainModel> GetDicByTypeAndValueAsync(string dicType, string dicValue)
        {
            var filter = new SingleQueryFilter();
            if (!string.IsNullOrWhiteSpace(dicType))
            {
                filter.AddEqual(nameof(InfraDic.Type), dicType);
            }
            if (!string.IsNullOrWhiteSpace(dicValue))
            {
                filter.AddEqual(nameof(InfraDic.Value), dicValue);
            }
            InfraDic infraDic = await _repository.QueryFirstOrDefaultAsync(filter.IsEmpty ? null : filter);

            return _mapper.Map<InfraDicDomainModel>(infraDic);
        }

        /// <summary>
        /// 根据类型和键值获取字典
        /// </summary>
        /// <param name="dicType"></param>
        /// <param name="dicKey"></param>
        /// <returns></returns>
        public async Task<List<InfraDicDomainModel>> GetDicListByTypeAsync(string dicType)
        {
            var filter = new SingleQueryFilter();
            if (!string.IsNullOrWhiteSpace(dicType))
            {
                filter.AddEqual(nameof(InfraDic.Type), dicType);
            }
            var res = await _repository.QueryAsync(filter);
            var list = _mapper.Map<List<InfraDicDomainModel>>(res.ToList());
            return list;
        }
    }
}
