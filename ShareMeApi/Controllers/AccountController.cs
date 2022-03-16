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
    /// <summary>
    /// 身份相关控制器
    /// </summary>
    [ApiController]
    [CatchExceptionFilter]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IJwtGetToken _jwtGetToken;
        private readonly ShareMeDBContext _dBContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jwtGetToken"></param>
        /// <param name="logger"></param>
        /// <param name="dBContext"></param>
        public AccountController(IJwtGetToken jwtGetToken,ILogger<AccountController> logger,ShareMeDBContext dBContext)
        {
            _logger = logger;
            _jwtGetToken = jwtGetToken;
            _dBContext = dBContext;
        }
        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 获取需要身份认证的数据
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 获取公开的数据
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 需要参数的公开测试方法
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 返回字符串的公开测试方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BaseModelResultFilter]
        public string GetStr()
        {
            return "啊";
        }
        /// <summary>
        /// 返回JsonResult的公开测试方法
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BaseModelResultFilter]
        public JsonResult GetStrV()
        {
            return new JsonResult("{name:abc}");
        }
    }
}
