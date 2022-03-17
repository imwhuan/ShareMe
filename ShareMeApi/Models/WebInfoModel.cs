namespace ShareMeApi.Models
{
    /// <summary>
    /// 网站基础信息
    /// </summary>
    public class WebInfoModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 图片服务地址
        /// </summary>
        public string? ImageServer { get; set; }

        /// <summary>
        /// 图标地址
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 登录背景图
        /// </summary>
        public string? BgLogin { get; set; }

        /// <summary>
        /// 主页欢迎文字
        /// </summary>
        public string? WelCome { get; set; }

    }
}
