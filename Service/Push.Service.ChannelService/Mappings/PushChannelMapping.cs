using Push.Service.ChannelService.DBModel;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.ChannelService.Mappings
{
    /// <summary>
    /// 推送通道
    /// </summary>
    public class PushChannelMapping : DapperMetadataProvider<PushChannel>
    {
        protected override void ConfigureModel(DapperMetadataBuilder<PushChannel> builder)
        {
            builder.TableName("push_channel");
            builder.HasKey(s => new { s.Id });//主键
        }
    }
}
