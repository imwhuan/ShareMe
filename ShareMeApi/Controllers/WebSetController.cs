using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareMeApi.DBContext;
using ShareMeApi.Filters;
using ShareMeApi.Models;
using ShareMeApi.Models.DBModel;
using System.Reflection;
using System.Linq;

namespace ShareMeApi.Controllers
{
    /// <summary>
    /// 对web提供基础配置服务
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [CatchExceptionFilter]
    public class WebSetController : ControllerBase
    {
        private readonly ShareMeDBContext DBContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_dBContext"></param>
        public WebSetController(ShareMeDBContext _dBContext)
        {
            DBContext=_dBContext;
        }

        /// <summary>
        /// 获取系统变量
        /// </summary>
        /// <param name="id">系统标识</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [BaseModelResultFilter]
        public Dictionary<string, string> GetSysSetting(string id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach(SysSetting sys in DBContext.SysSettings.Where(s => s.SysName == id).ToArray())
            {
                if (sys.SysKey!=null)
                {
                    dic.Add(sys.SysKey, sys.SysValue ?? "");
                }
            }
            return dic;
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetWebInfo/{id}")]
        [BaseModelResultFilter]
        public WebInfoModel GetWebInfo(string id)
        {
            WebInfoModel model = new();
            SysSetting[] sysSettings = DBContext.SysSettings.Where(s => s.SysName == id).ToArray();
            foreach (PropertyInfo prop in typeof(WebInfoModel).GetProperties())
            {
                prop.SetValue(model, sysSettings.Where(s => s.SysKey == prop.Name).Select(sys => sys.SysValue).FirstOrDefault());
            }
            return model;
        }
    }
}
