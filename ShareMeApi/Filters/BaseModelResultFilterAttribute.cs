using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShareMeApi.Models;

namespace ShareMeApi.Filters
{
    /// <summary>
    /// Result过滤器格式化数据返回格式
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class BaseModelResultFilterAttribute : Attribute, IResultFilter
    {
        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("进入BaseModelResultFilterAttribute-OnResultExecuted");
        }

        /// <summary>
        /// 格式化返回结果（仅格式化action返回类型为ObjectResult的结果）
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("进入BaseModelResultFilterAttribute-OnResultExecuting");
            if (context.Result is ObjectResult ores)
            {
                if(ores.DeclaredType != typeof(BaseResponseModel))
                {
                    context.Result = new ObjectResult(new BaseResponseModel()
                    {
                        Success = true,
                        StatusCode = StatusCodes.Status200OK,
                        Data = ores.Value,
                        Message = ""
                    });
                }
            }
        }
    }
}
