using System;
using System.Linq;
using System.Collections.Generic;
using Push.Api.Service.SendMsgHandler;
using AutoMapper;
using Push.Service.ProcessService.Service;
using Push.Api.DTOs;
using Push.Api.Enum;
using Push.Service.ProcessService.DomainModel;

namespace Push.Api.Service.SendMsgHandler
{
    public class RetryTimesOverHandler : IRetryTimesOverHandler
    {
        private IMapper _mapper;
        private IPushProcessHistoryService _pushProcessHistoryService;
        private IPushSendProcessService _pushSendProcessService;
        public RetryTimesOverHandler(IMapper mapper, IPushProcessHistoryService pushProcessHistoryService) {

            _mapper = mapper;
            _pushProcessHistoryService = pushProcessHistoryService;
        }
        public  bool Handle(SendProcessDto process, out string retMsg)
        {
            ProcessHistoryDto processHistoryDto = _mapper.Map<ProcessHistoryDto> (process);
            processHistoryDto.Id = 0;
            processHistoryDto.SendStatus = (int)SendStatusEnum.Failure;
            processHistoryDto.ErrorType = (int)ErrorTypeEnum.TryTimesOver;
            AddProcessHistoryDomainModel model = _mapper.Map<AddProcessHistoryDomainModel>(processHistoryDto);
            int row= _pushProcessHistoryService.InsertProcessHistoryAsync(model).Result;
            row= _pushSendProcessService.DeleteProcessByIdAsync(process.Id).Result;
            retMsg = "";
            return true;
        }
        /// <summary>
        /// 批处理，重试推送次数过多
        /// </summary>
        /// <param name="processList"></param>
        public  bool Handle(List<SendProcessDto> processList, out string retMsg)
        {
            retMsg = "";
            List<ProcessHistoryDto> processHistoryDtoList =_mapper.Map<List<ProcessHistoryDto>>(processList);
            processHistoryDtoList.ForEach(e => {
                e.SendStatus = (int)SendStatusEnum.Failure;
                e.ErrorType = (int)ErrorTypeEnum.TryTimesOver;
            });
            List<AddProcessHistoryDomainModel> modelList = _mapper.Map<List<AddProcessHistoryDomainModel>>(processHistoryDtoList);
            int rows = _pushProcessHistoryService.InsertProcessHistoryListAsync(modelList).Result;
            List<long> ids = processList.Select(t => t.Id).ToList();
            int deleteRows= _pushSendProcessService.DeleteProcessByIdsAsync(ids).Result;
            return true;
        }
    }
}
