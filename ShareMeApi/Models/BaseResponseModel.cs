namespace ShareMeApi.Models
{
    /// <summary>
    /// 数据返回格式
    /// </summary>
    public class BaseResponseModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set;}
        /// <summary>
        /// 附带消息
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object? Data { get; set;}
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool? Success { get; set;}
    }
}
