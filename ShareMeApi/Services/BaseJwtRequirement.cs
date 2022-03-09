using Microsoft.AspNetCore.Authorization;

namespace ShareMeApi.Services
{
    public class BaseJwtRequirement: IAuthorizationRequirement
    {
    }
    public class FaileResultHandler : AuthorizationHandler<BaseJwtRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BaseJwtRequirement requirement)
        {
            System.Console.WriteLine("进入 FaileResultHandler 方法");
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
