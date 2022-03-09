using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ShareMeApi.IServices;
using ShareMeApi.Services;
using ShareMeApi.Models;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//添加控制器服务
builder.Services.AddControllers();
//读取Jwt配置文件到容器内
builder.Services.Configure<JwtTokenConfigModel>(builder.Configuration.GetSection("JwtTokenConfig"));
//注册自定义的获取jwt Token的方法到容器服务
builder.Services.AddSingleton<IJwtGetToken, JwtGetToken>();
builder.Services.AddSingleton<IAuthorizationHandler, FaileResultHandler>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//添加Swagger/OpenAPI服务，展示接口信息用的
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//添加身份验证（使用jwt并进行配置）
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, "my_jwt_handle", option =>
     {
         option.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidIssuer = builder.Configuration["JwtTokenConfig:Issuer"],
             ValidateAudience = true,
             ValidAudience = builder.Configuration["JwtTokenConfig:Issuer"],
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtTokenConfig:SecurityKey"])),
             AudienceValidator = (audiences, securityToken, validationParameters) =>
             {
                 return true;
             },
             LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
             {
                 return true;
             }
         };
         //重写Jwt的OnChallenge事件，可以自定义验证不通过时的返回信息
         option.Events = new JwtBearerEvents {
             //OnAuthenticationFailed = context =>
             //{
             //   Console.WriteLine("OnAuthenticationFailed");
             //   return Task.CompletedTask;
             //},
             OnForbidden = context =>
             {
                 //权限验证未通过时调用（授权阶段失败）
                 //Console.WriteLine("OnForbidden");
                 //context.HandleResponse();
                 context.Response.ContentType = "application/json";
                 context.Response.StatusCode = StatusCodes.Status403Forbidden;
                 context.Response.WriteAsJsonAsync<BaseResponseModel>(new BaseResponseModel()
                 {
                     StatusCode = StatusCodes.Status403Forbidden,
                     Message = "不好意思你没有该功能的权限哦~~🤗",
                     Success = false
                 });
                 return Task.CompletedTask;
             },
             //OnMessageReceived=context =>
             //{
             //   //收到请求时调用
             //    Console.WriteLine("OnMessageReceived");
             //    return Task.CompletedTask;
             //},
             OnChallenge =context =>
             {
                 //token验证未通过时调用（鉴权/认证阶段失败）
                 //Console.WriteLine("OnChallenge");
                 context.HandleResponse();
                 context.Response.ContentType = "application/json";
                 context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                 context.Response.WriteAsJsonAsync<BaseResponseModel>(new BaseResponseModel()
                 {
                     StatusCode = StatusCodes.Status401Unauthorized,
                     Message = "不好意思你还没有通过身份验证哦~~🤗",
                     Success=false
                 });
                 return Task.CompletedTask;
             }
         };
     });

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("DiyAuthroization", builder =>
    {
        builder.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        builder.RequireClaim("LittleName", "imwhuan");
        builder.AddRequirements(new BaseJwtRequirement());
    });
});
//中间管道
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
