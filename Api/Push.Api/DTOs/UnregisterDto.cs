namespace Push.Api.DTOs
{
    public class UnregisterRequestDto
    {
        public string Token { get; set; }
        public int? AppId { get; set; }
        public string DeviceId { get; set; }
    }
}
