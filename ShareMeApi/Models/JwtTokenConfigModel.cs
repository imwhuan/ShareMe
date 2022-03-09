namespace ShareMeApi.Models
{
    public class JwtTokenConfigModel
    {
        /// <summary>
        /// token颁发组织
        /// </summary>
        public string? Issuer { get; set; }
        /// <summary>
        /// token授予组织
        /// </summary>
        public string? Audience { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string? SecurityKey { get; set; }
    }
}
