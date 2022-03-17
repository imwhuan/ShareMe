using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareMeApi.DBContext;
using ShareMeApi.Models.DBModel;

namespace ShareMeApi.Controllers
{
    /// <summary>
    /// 文件管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ShareMeDBContext DBContext;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_dBContext"></param>
        public FileController(ShareMeDBContext _dBContext)
        {
            DBContext = _dBContext;
        }
        /// <summary>
        /// 获取图片流
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Img/{id}")]
        public IActionResult GetImages(string id)
        {
            SysSetting? sysSetting = DBContext.SysSettings.Find("img", id);
            if(sysSetting == null || string.IsNullOrEmpty(sysSetting.SysValue))
            {
                return NotFound();
            }
            else
            {
                string FilePath = Path.Combine(AppContext.BaseDirectory, "Images", sysSetting.SysValue);
                if (System.IO.File.Exists(FilePath))
                {
                    Stream stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                    return File(System.IO.File.ReadAllBytes(FilePath), "image/png", sysSetting.SysValue);
                }
                else
                {
                    return NotFound();
                }
            }
        }
    }
}
