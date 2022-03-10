using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareMeApi.Filters;
using ShareMeApi.IServices;
using ShareMeApi.Models;

namespace ShareMeApi.Controllers
{
    [ApiController]
    [CatchExceptionFilter]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IJwtGetToken _jwtGetToken;
        public AccountController(IJwtGetToken jwtGetToken,ILogger<AccountController> logger)
        {
            _logger = logger;
            _jwtGetToken = jwtGetToken;
        }
        [HttpPost]
        public ActionResult<BaseResponseModel> Login([Bind("Name", "Password")] LoginModel model)
        {
            if (string.IsNullOrEmpty(model.Name) == false && string.IsNullOrEmpty(model.Password) == false && model.Name.Equals("imwhuan") && model.Password.Equals("1234"))
            {
                string token = _jwtGetToken.GetToken(model.Name, model.Password);
                BaseResponseModel res = new BaseResponseModel()
                {
                    StatusCode = StatusCodes.Status200OK,
                    Success = true,
                    Data=token
                };
                return Ok(res);
            }
            else
            {
                BaseResponseModel res = new BaseResponseModel()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Success = false,
                    Message= "账号或密码错误！😭"
                };
                return Unauthorized(res);
            }
        }
        [Authorize(Policy = "DiyAuthroization")]
        [HttpGet]
        public BaseResponseModel GetSecretData()
        {
            BaseResponseModel result = new BaseResponseModel()
            {
                StatusCode = 200,
                Success = true,
                Data = "给你一个私密的数据！😕"
            };
            return result;
        }
        [HttpGet]
        public BaseResponseModel GetPublicData()
        {
            BaseResponseModel result = new BaseResponseModel()
            {
                StatusCode = 200,
                Success = true,
                Data = "给你一个公开的数据！😕"
            };
            return result;
        }
        [HttpGet]
        [BaseModelResultFilter]
        public BaseResponseModel GetPublicDataByParam(string name)
        {
            _logger.LogInformation("进入GetPublicDataByParam");
            BaseResponseModel result = new BaseResponseModel()
            {
                StatusCode = 200,
                Success = true,
                Data = "给你一个公开的数据！😕"+ name
            };
            return result;
        }
        [HttpGet]
        [BaseModelResultFilter]
        public string GetStr()
        {
            return "啊";
        }
        [HttpGet]
        [BaseModelResultFilter]
        public JsonResult GetStrV()
        {
            return new JsonResult("{name:abc}");
        }
    }
}
