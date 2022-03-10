using System.IO;
namespace ShareMeApi.Services
{
    public class HttpClientHelper
    {
        private readonly HttpClient _httpClient;
        public HttpClientHelper()
        {
            _httpClient = new HttpClient();
        }
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
        public async Task<string> GetStringAsync(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }
        public async Task<T?> GetT<T>(string url)
        {
            return await _httpClient.GetFromJsonAsync<T>(url);
        }
    }
}
