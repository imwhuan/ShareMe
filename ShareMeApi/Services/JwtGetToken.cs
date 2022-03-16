using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ShareMeApi.IServices;
using ShareMeApi.Models;

namespace ShareMeApi.Services
{
    /// <summary>
    /// 生成Jwt的Token
    /// </summary>
    public class JwtGetToken : IJwtGetToken
    {
        private readonly JwtTokenConfigModel _jwtTokenConfig;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jwtTokenConfig"></param>
        public JwtGetToken(IOptions<JwtTokenConfigModel> jwtTokenConfig)
        {
            _jwtTokenConfig=jwtTokenConfig.Value;
        }
        /// <summary>
        /// 根据用户名密码生成Jwt的Token方法
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public string GetToken(string name, string password)
        {
            Claim[] claims = new Claim[]
           {
                new Claim(ClaimTypes.Name,name),
                new Claim(ClaimTypes.Email,"imwhuan@qq.com"),
                new Claim("LittleName","imwhuan"),
                new Claim(ClaimTypes.MobilePhone,"176307")
           };
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenConfig.SecurityKey??""));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtTokenConfig.Issuer, audience: _jwtTokenConfig.Audience, claims: claims,
                expires: System.DateTime.Now.AddMinutes(5), signingCredentials: signingCredentials);

            string resToken = new JwtSecurityTokenHandler().WriteToken(token);
            return resToken;
        }
    }
}
