using Push.Service.ProcessService.DBModel;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.ProcessService.Service
{
    /// <summary>
    /// 字典信息
    /// </summary>
    public class PushProcessHistoryMapping : DapperMetadataProvider<PushProcessHistory>
    {
        protected override void ConfigureModel(DapperMetadataBuilder<PushProcessHistory> builder)
        {
            builder.TableName("push_process_history");
            builder.HasKey(s => new { s.Id });//主键
        }
    }
}
