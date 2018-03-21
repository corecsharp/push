using Push.Service.TokenService.DBModel;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.TokenService.Mappings
{
    /// <summary>
    /// 手机设备注册明细，包括几个推送平台的token
    /// </summary>
    public class PushTokenBrandDetailMapping : DapperMetadataProvider<PushTokenBrandDetail>
    {
        protected override void ConfigureModel(DapperMetadataBuilder<PushTokenBrandDetail> builder)
        {
            builder.TableName("push_token_brand_detail");
            builder.HasKey(s => new { s.Id });//主键
        }
    }
}
