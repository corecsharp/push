using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Push.Service.ProcessService.DBModel;
using Sherlock.Framework;
using Microsoft.Extensions.Logging;
using Dapper;
using AutoMapper;
using Push.Service.ProcessService.Repository;
using Sherlock.Framework.Services;
using Platform.Core.Helper;
using Push.Service.ProcessService.DomainModel;

namespace Push.Service.ProcessService.Service
{
    /// <summary>
    /// 待推送的消息，如果一个token对应多个手机，则一条消息拆成多条去发，如果发送失败，回写待发送消息，记录哪个手机设备发送失败。
    /// </summary>
    public class PushSendProcessService : IPushSendProcessService
    {
        private IRepository<PushSendProcess> _repository;
        private IPushSendProcessRepository _pushSendProcessRepository;
        private IMapper _mapper = null;
        private IIdGenerationService _idGenerator;


        public PushSendProcessService(IMapper mapper
            , IRepository<PushSendProcess> repository
            , IPushSendProcessRepository pushSendProcessRepository
            , IIdGenerationService idGenerator
            )
        {
            _mapper = mapper;
            _repository = repository;
            _pushSendProcessRepository = pushSendProcessRepository;
            _idGenerator = idGenerator;
        }

        /// <summary>
        /// 单个新增推送消息
        /// </summary>
        /// <returns></returns>
        public async Task<int> InsertProcessAsync(PushSendProcessDomainModel domainModel)
        {
            if (domainModel == null) return 0;
            domainModel.Id = _idGenerator.GenerateId();
            PushSendProcess entity = _mapper.Map<PushSendProcess>(domainModel);
            entity.CreateAt = entity.UpdateAt = DateTimeHelper.GetNow();
            return await _repository.InsertAsync(entity);
        }

        public async Task<int> DeleteProcessByIdAsync(long id)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushSendProcess.Id), id);
            return await _repository.DeleteAsync(filter);
        }

        public async Task<int> DeleteProcessByIdsAsync(List<long> ids)
        {
            int totalCount = 0;
            foreach (var id in ids)
            {
                var filter = new SingleQueryFilter();
                filter.AddEqual(nameof(PushSendProcess.Id), id);
                int row = await _repository.DeleteAsync(filter);
                totalCount = totalCount + row;
            }
            return totalCount;
        }

        public async Task<PushSendProcessAppChannelDomainModel> GetAppIdAndChannelIdAsync(DateTime timeNow)
        {
            var domainModel = await _pushSendProcessRepository.GetAppIdAndChannelIdAsync(timeNow);

            return domainModel;
        }

        public async Task<PushSendProcessDomainModel> GetProcessByIdAsync(long processId)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushSendProcess.Id), processId);
            PushSendProcess entity = await _repository.QueryFirstOrDefaultAsync(filter);
            return _mapper.Map<PushSendProcessDomainModel>(entity);
        }

        public async Task<List<PushSendProcessDomainModel>> GetProcessListByBatchNOAsync(string batchNO)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushSendProcess.BatchNo), batchNO);
            var res = await _repository.QueryAsync(filter);
            List<PushSendProcess> list = res.ToList();

            return _mapper.Map<List<PushSendProcessDomainModel>>(list);
        }

        public async Task<int> GetSendProcessCountAsync()
        {
            return await _repository.CountAsync();
        }

        public async Task<int> InsertProcessListAsync(List<PushSendProcessDomainModel> pushList)
        {
            if (pushList == null) return 0;
            List<PushSendProcess> list = _mapper.Map<List<PushSendProcess>>(pushList);
            foreach (var item in list)
            {
                item.Id = _idGenerator.GenerateId();
                item.CreateAt = item.UpdateAt = DateTimeHelper.GetNow();
            }
            int totalCount = _repository.Insert(list);
            return totalCount;
        }

        public async Task<List<long>> SelectProcessIdListByBatchNOAsync(Guid guid)
        {
            var filter = new SingleQueryFilter();
            filter.AddEqual(nameof(PushSendProcess.BatchNo), guid);
            var res = await _repository.QueryAsync(filter);
            return res?.Select(i => i.Id).ToList();
        }

        public async Task<int> UpdateBatchNOAsync(Guid guid, DateTime expireTime)
        {
            string bacthNo = guid.ToString();
            return await _pushSendProcessRepository.UpdateBatchNOAsync(bacthNo, expireTime);
        }

        public async Task<int> UpdateBatchNOByAppIdAndChannelIdAsync(BatchProcessParmsDomainModel domainModel)
        {



            return await _pushSendProcessRepository.UpdateBatchNOByAppIdAndChannelIdAsync(domainModel);

        }

        public async Task<int> UpdateProcessUseStatusByIdAsync(long processId, DateTime expireTime)
        {
            return await _pushSendProcessRepository.UpdateProcessUseStatusByIdAsync(processId, expireTime);
        }

        public async Task<int> WriteBackProcessByIdAsync(long id, DateTime sendTime)
        {
            return await _pushSendProcessRepository.WriteBackProcessByIdAsync(id, sendTime);
        }

        public async Task<int> WriteBackProcessByIdsAsync(List<long> ids, DateTime sendTime)
        {
            return await _pushSendProcessRepository.WriteBackProcessByIdsAsync(ids, sendTime);
        }
    }
}