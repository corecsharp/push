using Push.Service.ConfigService.DBModel;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.ConfigService.Mappings
{
    /// <summary>
    /// 字典信息
    /// </summary>
    public class InfraDicMapping : DapperMetadataProvider<InfraDic>
    {
        protected override void ConfigureModel(DapperMetadataBuilder<InfraDic> builder)
        {
            builder.TableName("infra_dic");
            builder.HasKey(s => new { s.Id });//主键
        }
    }
}
