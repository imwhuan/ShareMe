using System.IO;
namespace ShareMeApi.Services
{
    /// <summary>
    /// HttpClient辅助程序
    /// </summary>
    public class HttpClientHelper
    {
        private readonly HttpClient _httpClient;
        /// <summary>
        /// 构造函数
        /// </summary>
        public HttpClientHelper()
        {
            _httpClient = new HttpClient();
        }
        /// <summary>
        /// 获取Stream
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public async Task<Stream> GetStream(string url)
        {
            //_httpClient.DefaultRequestHeaders.Accept.Clear();
            //_httpClient.DefaultRequestHeaders.Accept.Add(
            //    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            //_httpClient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            //Task<string> stringTask = _httpClient.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
            //string msg = await stringTask;
            //Console.WriteLine(msg);

            //Repository a = JsonConvert.DeserializeObject<Repository>("你的字符串");

            return await _httpClient.GetStreamAsync(url);
        }
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> GetStringAsync(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }
        /// <summary>
        /// 泛型获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<T?> GetT<T>(string url)
        {
            return await _httpClient.GetFromJsonAsync<T>(url);
        }
    }
}
