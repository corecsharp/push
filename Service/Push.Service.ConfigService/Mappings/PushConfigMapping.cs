using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Push.Service.ConfigService.DBModel;

namespace Push.Service.ConfigService
{
    /// <summary>
    /// 推送配置
    /// </summary>
    public class PushConfigMapping: DapperMetadataProvider<PushConfig>
    {
	    protected override void ConfigureModel(DapperMetadataBuilder<PushConfig> builder)
        {
            builder.TableName("push_config");
            builder.HasKey(s => new { s.Id });//主键
        }
    }
}
