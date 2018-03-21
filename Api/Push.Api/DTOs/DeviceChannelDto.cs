namespace Push.Api.DTOs
{
    /// <summary>
    /// 设备通道信息
    /// </summary>
    public class DeviceChannelDto
    {
        /// <summary>
        /// TokenBrandId
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 设备Token
        /// </summary>
        public string DeviceToken { get; set; }

        /// <summary>
        /// 手机类型
        /// </summary>
        public int SystemType { get; set; }

        /// <summary>
        /// 手机品牌Id
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// 通道名称
        /// </summary>
        public string ChannelName { get; set; }

        /// <summary>
        /// 通道的Id
        /// </summary>
        public long ChannelId { get; set; }

        /// <summary>
        /// 通道的URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// App在通道下对应的AppKey
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// App在通道下对应的AppSecret
        /// </summary>
        public string AppSecret { get; set; }
    }
}
