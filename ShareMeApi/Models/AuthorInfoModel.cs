namespace ShareMeApi.Models
{
    /// <summary>
    /// 作者信息
    /// </summary>
    public class AuthorInfoModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// Github主页
        /// </summary>
        public string? GitHub { get; set; }
        /// <summary>
        /// 网站
        /// </summary>
        public string? HostUrl { get; set; }
        /// <summary>
        /// 项目标题
        /// </summary>
        public string? ProTitle { get; set; }
        /// <summary>
        /// 项目描述
        /// </summary>
        public string? ProDesc { get; set; }
    }
}
