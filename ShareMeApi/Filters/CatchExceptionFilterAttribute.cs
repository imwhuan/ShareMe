using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShareMeApi.Models;

namespace ShareMeApi.Filters
{
    public class CatchExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //Console.WriteLine("捕获到了异常");
            context.ExceptionHandled = true;
            ObjectResult result = new(new BaseResponseModel()
            {
                Success = false,
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = context.Exception.Message
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
            context.Result = result;
            //context.Result = new UnauthorizedObjectResult(resmodel);
        }
    }
}
