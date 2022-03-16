using System.ComponentModel.DataAnnotations;

namespace ShareMeApi.Models
{
    /// <summary>
    /// 登录注册模板
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string? Name { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        public string? Password { get; set; }
    }
}
