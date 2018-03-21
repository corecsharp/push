using Push.Service.ProcessService.DBModel;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.ProcessService.Service
{
    /// <summary>
    /// 待推送的消息，如果一个token对应多个手机，则一条消息拆成多条去发，如果发送失败，回写待发送消息，记录哪个手机设备发送失败。
    /// </summary>
    public class PushSendProcessMapping: DapperMetadataProvider<PushSendProcess>
    {
	    protected override void ConfigureModel(DapperMetadataBuilder<PushSendProcess> builder)
        {
            builder.TableName("push_send_process");
            builder.HasKey(s => new { s.Id });//主键
        }
    }
}
