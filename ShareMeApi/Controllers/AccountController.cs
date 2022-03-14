using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareMeApi.DBContext;
using ShareMeApi.Filters;
using ShareMeApi.IServices;
using ShareMeApi.Models;
using ShareMeApi.Models.DBModel;

namespace ShareMeApi.Controllers
{
    [ApiController]
    [CatchExceptionFilter]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IJwtGetToken _jwtGetToken;
        private readonly ShareMeDBContext _dBContext;
        public AccountController(IJwtGetToken jwtGetToken,ILogger<AccountController> logger,ShareMeDBContext dBContext)
        {
            _logger = logger;
            _jwtGetToken = jwtGetToken;
            _dBContext = dBContext;
        }
        [HttpPost]
        public ActionResult<BaseResponseModel> Login([Bind("Name", "Password")] LoginModel model)
        {
            BaseResponseModel res = new BaseResponseModel()
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Success = false,
            };
            if (string.IsNullOrEmpty(model.Name) == false && string.IsNullOrEmpty(model.Password)==false)
            {
                UserInfo? user = _dBContext.UserInfos.Where(x => x.Name == model.Name).FirstOrDefault();
                if (user is null)
                {
                    res.Message = $"{model.Name}用户不存在无法登录！😭";
                }
                else
                {
                    if(user.Password == model.Password)
                    {
                        string token = _jwtGetToken.GetToken(model.Name, model.Password);

                        res.StatusCode = StatusCodes.Status200OK;
                        res.Success = true;
                        res.Data = token;
                        return Ok(res);
                    }
                    else
                    {
                        res.Message = "密码错误！";
                    }
                }
                
            }
            else
            {
                res.Message = "账号或密码不能为空！😭";
            }
            return Unauthorized(res);
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BaseResponseModel> Register([Bind("Name", "Password")] LoginModel model)
        {
            BaseResponseModel res = new BaseResponseModel()
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Success = false,
            };
            if (string.IsNullOrEmpty(model.Name) == false && string.IsNullOrEmpty(model.Password) == false)
            {
                if(_dBContext.UserInfos.Any(x => x.Name == model.Name))
                {
                    res.Message = $"{model.Name}用户已存在无法重复注册！😭";
                }
                else
                {
                    UserInfo newUser = new UserInfo()
                    {
                        Name = model.Name,
                        Password = model.Password
                    };
                    _dBContext.UserInfos.Add(newUser);
                    if (_dBContext.SaveChanges() == 1)
                    {
                        res.StatusCode = StatusCodes.Status200OK;
                        res.Success = true;
                        res.Data = $"{model.Name}注册成功！";
                        return Ok(res);
                    }
                }
            }
            else
            {
                res.Message = "账号或密码不能为空！😭";
            }
            return Unauthorized(res);
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
