using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Push.Service.TokenService.DomainModel
{
    /// <summary>
    ///注册请求
    /// </summary>
    public class RegisterRequestDomainModel
    {

        public string Token { get; set; }

        public int? AppId { get; set; }

        public string DeviceId { get; set; }

        public string Brand { get; set; }

        public int BrandId { get; set; }

        public int SystemType { get; set; }

        public List<PushTokenBrandDetailDomainModel> DeviceTokens { get; set; }
    }
}
