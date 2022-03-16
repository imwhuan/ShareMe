using Microsoft.AspNetCore.Authorization;

namespace ShareMeApi.Services
{
    /// <summary>
    /// Jwt自定义验证
    /// </summary>
    public class BaseJwtRequirement: IAuthorizationRequirement
    {
    }
    /// <summary>
    /// Jwt自定义验证处理器
    /// </summary>
    public class FaileResultHandler : AuthorizationHandler<BaseJwtRequirement>
    {
        /// <summary>
        /// Jwt自定义验证处理器
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BaseJwtRequirement requirement)
        {
            System.Console.WriteLine("进入 FaileResultHandler 方法");
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
