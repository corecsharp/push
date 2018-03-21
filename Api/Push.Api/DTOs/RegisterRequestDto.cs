using System.Collections.Generic;

namespace Push.Api.DTOs
{
    public class RegisterRequestDto
    {
        public string Token { get; set; }

        public int? AppId { get; set; }

        public string DeviceId { get; set; }

        public string Brand { get; set; }

        public List<TokenBrandDetailDto> DeviceTokens { get; set; }
    }
}
