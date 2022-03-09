namespace ShareMeApi.IServices
{
    public interface IJwtGetToken
    {
        /// <summary>
        /// 根据用户名密码创建token
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public string GetToken(string name, string password);
    }
}
