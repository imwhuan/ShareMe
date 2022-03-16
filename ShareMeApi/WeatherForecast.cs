namespace ShareMeApi
{
    /// <summary>
    /// 天气信息
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 天气信息
        /// </summary>
        public int TemperatureC { get; set; }
        /// <summary>
        /// 天气信息
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        /// <summary>
        /// 天气信息
        /// </summary>
        public string? Summary { get; set; }
    }
}