using Push.Service.ChannelService.DBModel;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.ChannelService.Mappings
{
    /// <summary>
    /// app走的推送通道
    /// </summary>
    public class PushAppChannelMapping : DapperMetadataProvider<PushAppChannel>
    {
        protected override void ConfigureModel(DapperMetadataBuilder<PushAppChannel> builder)
        {
            builder.TableName("push_app_channel");
            builder.HasKey(s => new { s.Id });//主键
        }
    }
}
