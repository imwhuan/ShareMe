namespace ShareMeApi
{
    /// <summary>
    /// ������Ϣ
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// ����
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public int TemperatureC { get; set; }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string? Summary { get; set; }
    }
}