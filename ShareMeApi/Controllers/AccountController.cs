using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareMeApi.IServices;

namespace ShareMeApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IJwtGetToken _jwtGetToken;
        public AccountController(IJwtGetToken jwtGetToken)
        {
            _jwtGetToken=jwtGetToken;
        }
        [HttpGet]
        public string Index()
        {
            return "欢迎使用XToo🚀";
        }
        [HttpPost]
        public IActionResult Login(string name, string password)
        {
            if (name.Equals("imwhuan") && password.Equals("1234"))
            {
                string token = _jwtGetToken.GetToken(name, password);
                return Ok(new
                {
                    state = 0,
                    token
                });
            }
            else
            {
                return Unauthorized("账号或密码错误！😭");
            }
        }
        [Authorize(Policy = "DiyAuthroization")]
        [HttpGet]
        public IActionResult GetData()
        {
            return Ok("给你数据！😕");
        }
    }
}
