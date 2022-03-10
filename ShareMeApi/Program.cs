using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ShareMeApi.IServices;
using ShareMeApi.Services;
using ShareMeApi.Models;
using Microsoft.AspNetCore.Authorization;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

#region 配置NLog

builder.Logging.ClearProviders().SetMinimumLevel(LogLevel.Trace);
builder.Host.UseNLog();
//指定NLog配置文件，默认使用根目录的nlog.config文件（全小写）
NLogBuilder.ConfigureNLog("Conf/NLog.config");

#endregion

#region IOC服务注入

builder.Services.AddControllers();
//读取Jwt配置文件到容器内
builder.Services.Configure<JwtTokenConfigModel>(builder.Configuration.GetSection("JwtTokenConfig"));
//注册自定义的获取jwt Token的方法到容器服务
builder.Services.AddSingleton<IJwtGetToken, JwtGetToken>();
builder.Services.AddSingleton<IAuthorizationHandler, FaileResultHandler>();
builder.Services.AddSingleton<HttpClientHelper>();
#endregion

#region 添加Swagger/OpenAPI服务，展示接口信息用的
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region 配置Jwt身份验证

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
        option.Events = new JwtBearerEvents
        {
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
            OnChallenge = context =>
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
                    Success = false
                });
                return Task.CompletedTask;
            }
        };
    });

#endregion

#region 自定义授权策略
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("DiyAuthroization", builder =>
    {
        builder.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        builder.RequireClaim("LittleName", "imwhuan");
        builder.AddRequirements(new BaseJwtRequirement());
    });
});
#endregion

#region 支持跨域

string MyCor = "MyCor";
builder.Services.AddCors(option =>
{
    option.AddPolicy(MyCor, builder =>
    {
        builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});

#endregion

//中间管道
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();
//https重定向
app.UseHttpsRedirection();

app.UseCors(MyCor);
//身份验证中间件
app.UseAuthentication();
app.UseAuthorization();

//配置路由
app.MapGet("/", context =>
{
    context.Response.ContentType = "text/plain;charset=utf-8";
    string res= "欢迎使用ShareMe！~~O(∩_∩)O~~ 🚀  \r\n系统接口详情请参阅swagger文档：" + context.Request.Host.Value + "/swagger";
    return context.Response.WriteAsync(res,Encoding.UTF8);
});
app.MapControllers();

//启动服务（程序起点）
app.Run();
