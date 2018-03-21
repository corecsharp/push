using Push.Service.TokenService.DBModel;
using Sherlock.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Push.Service.TokenService.Mappings
{
    /// <summary>
    /// 手机设备注册
    /// </summary>
    public class PushTokenBrandMapping : DapperMetadataProvider<PushTokenBrand>
    {
        protected override void ConfigureModel(DapperMetadataBuilder<PushTokenBrand> builder)
        {
            builder.TableName("push_token_brand");
            builder.HasKey(s => new { s.Id });//主键
        }
    }
}
