using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Sherlock.Framework.Services;
using Platform.Core.Helper;
using Push.Service.ProcessService.DBModel;
using Push.Service.ProcessService.DomainModel;

namespace Push.Service.ProcessService.Service
{
    /// <summary>
    /// 推送消息历史发送记录。记录，无论成功或失败。
    /// </summary>
    public class PushProcessHistoryService : IPushProcessHistoryService
    {
        private IRepository<PushProcessHistory> _repository;
        private IMapper _mapper = null;
        private IIdGenerationService _idGenerator;
        public PushProcessHistoryService(IMapper mapper, IRepository<PushProcessHistory> repository, IIdGenerationService idGenerator)
        {
            _mapper = mapper;
            _repository = repository;
            _idGenerator = idGenerator;
        }

       public async Task<int> InsertProcessHistoryAsync(AddProcessHistoryDomainModel dto)
        {
            if (dto == null) return 0;
            dto.Id = _idGenerator.GenerateId();
            PushProcessHistory entity = _mapper.Map<PushProcessHistory>(dto);
            entity.CreateAt=entity.UpdateAt = DateTimeHelper.GetNow();
            if (entity.DeviceToken.IsNullOrEmpty()) {
                entity.DeviceToken = "";
            }
            int row = await _repository.InsertAsync(entity);
            return row;
        }

        public async Task<int> GetSendFailureCountAsync(DateTime beginTime, DateTime endTime)
        {
            var filter = new SingleQueryFilter();
            filter.AddGreaterOrEqual(nameof(PushProcessHistory.SendTime), beginTime);
            filter.AddLessOrEqual(nameof(PushProcessHistory.SendTime), endTime);

            return await _repository.CountAsync(filter);
        }

        public async Task<int> InsertProcessHistoryListAsync(List<AddProcessHistoryDomainModel> historyList)
        {
            if (historyList == null) return 0;
            List<PushProcessHistory> list = _mapper.Map<List<PushProcessHistory>>(historyList);
            var totalCount = 0;
            foreach (var item in list)
            {
                item.Id = _idGenerator.GenerateId();
                item.CreateAt = item.UpdateAt = DateTimeHelper.GetNow();
            }
            totalCount = _repository.Insert(list);
            return totalCount;
        }
    }
}