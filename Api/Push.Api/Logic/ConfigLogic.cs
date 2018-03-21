using AutoMapper;
using Push.Api.DTOs;
using Push.Core.DTOs;
using Push.Service.ConfigService;
using Push.Service.ConfigService.DomainModel;
using System;

namespace Push.Api.Logic
{
    /// <summary>
    /// 获取字典逻辑
    /// </summary>
    public class ConfigLogic: IConfigLogic
    {
        private IPushConfigService _pushConfigService;
        private IMapper _mapper;
        public ConfigLogic(IPushConfigService pushConfigService,IMapper mapper)
        {
            _pushConfigService = pushConfigService;
            _mapper = mapper;
        }


        public ConfigDto GetConfig(string config)
        {
            return CourseCacheLogic<string, ConfigDto>.Get(config, () => {
                PushConfigDomainModel domainModel = _pushConfigService.GetConfigByKeyAsync(config).Result;
                return _mapper.Map<ConfigDto>(domainModel);
            });
        }


        public object GetConfigValue(string config)
        {
            ConfigDto configDto = GetConfig(config);
            if (configDto == null) {
                //LogHelper.Error.Write("GetConfigValue", string.Format("Config参数：{0}未找到", config));
                return null;
            }
            if (!configDto.IsActive)
            {
                //LogHelper.Error.Write("GetConfigValue", string.Format("Config参数：{0}未启用", config));
                return null;
            }
            return configDto.ConfigValue;
        }
    }


    public class ConfigKey {
        public const string ProductionMode = "ProductionMode";//推送调试模式
        public const string TokenMaxNum = "TokenMaxNum";//一次接收推送人上限
        public const string IsRealPushMsg = "IsRealPushMsg";//推送开关
        public const string SemaphoreCount = "SemaphoreCount";//信号量
        public const string MonitorTime = "MonitorTime";//监控时间段(单位：分钟)

    }
}
