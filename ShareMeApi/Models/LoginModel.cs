using System.ComponentModel.DataAnnotations;

namespace ShareMeApi.Models
{
    public class LoginModel
    {
        [Display(Name = "用户名")]
        public string? Name { get; set; }
        [Display(Name = "密码")]
        public string? Password { get; set; }
    }
}
