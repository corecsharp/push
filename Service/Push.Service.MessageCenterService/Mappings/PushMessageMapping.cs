using Push.Service.MessageCenterService.DBModel;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.MessageCenterService.Mappings
{
    public class PushMessageMapping : DapperMetadataProvider<PushMessage>
    {
        protected override void ConfigureModel(DapperMetadataBuilder<PushMessage> builder)
        {
            builder.TableName("push_message");
            builder.HasKey(s => new { s.Id });//主键
        }
    }
}
