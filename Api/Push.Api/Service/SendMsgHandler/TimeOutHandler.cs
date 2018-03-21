using System;
using System.Linq;
using System.Collections.Generic;
using Push.Core.DTOs;
using AutoMapper;
using Push.Service.TokenService.Service;
using Push.Api.Logic;
using Push.Service.ProcessService.Service;
using Push.Api.DTOs;
using Push.Api.Enum;
using Push.Service.ProcessService.DomainModel;

namespace Push.Api.Service.SendMsgHandler
{
    /// <summary>
    /// 超时处理
    /// </summary>
    public class TimeOutHandler : ITimeOutHandler
    {


        private IMapper _mapper;
        private IPushTokenBrandService _pushTokenBrandService;
        private ISendProcessLogic _sendProcessLogic;
        private IPushSendProcessService _pushSendProcessService;
        private IPushProcessHistoryService _pushProcessHistoryService;

        public TimeOutHandler(IMapper mapper
            , IPushTokenBrandService pushTokenBrandService
            , ISendProcessLogic sendProcessLogic
            , IPushSendProcessService pushSendProcessService
             , IPushProcessHistoryService pushProcessHistoryService
            )
        {
            _mapper = mapper;
            _pushTokenBrandService = pushTokenBrandService;
            _sendProcessLogic = sendProcessLogic;
            _pushSendProcessService = pushSendProcessService;
            _pushProcessHistoryService = pushProcessHistoryService;
        }

        /// <summary>
        /// 处理方法
        /// </summary>
        /// <param name="process"></param>
        public  bool Handle(SendProcessDto process, out string retMsg)
        {
            ProcessHistoryDto processHistoryDto = _mapper.Map<ProcessHistoryDto>(process);
            processHistoryDto.Id = 0;
            processHistoryDto.SendStatus = (int)SendStatusEnum.Failure;
            processHistoryDto.ErrorType = (int)ErrorTypeEnum.TimeOut;
            AddProcessHistoryDomainModel model = _mapper.Map<AddProcessHistoryDomainModel>(processHistoryDto);
            int row = _pushProcessHistoryService.InsertProcessHistoryAsync(model).Result;
            row = _pushSendProcessService.DeleteProcessByIdAsync(process.Id).Result;
            retMsg = "";
            return true;
        }


        /// <summary>
        /// 批处理，超时推送
        /// </summary>
        /// <param name="processList"></param>
        public  bool Handle(List<SendProcessDto> processList, out string retMsg)
        {
            retMsg = "";
            List<ProcessHistoryDto> processHistoryDtoList = _mapper.Map<List<ProcessHistoryDto>>(processList);
            processHistoryDtoList.ForEach(e =>
            {
                e.SendStatus = (int)SendStatusEnum.Failure;
                e.ErrorType = (int)ErrorTypeEnum.TimeOut;
            });
            List<AddProcessHistoryDomainModel> modelList = _mapper.Map<List<AddProcessHistoryDomainModel>>(processHistoryDtoList);
            _pushProcessHistoryService.InsertProcessHistoryListAsync(modelList);
            List<long> ids = processList.Select(t => t.Id).ToList();
            int rows = _pushSendProcessService.DeleteProcessByIdsAsync(ids).Result;
            return true;
        }
    }
}
