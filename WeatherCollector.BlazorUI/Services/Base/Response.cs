namespace WeatherCollector.BlazorUI.Services.Base
{
    public class Response<T>
    {
        public string FaultMessage { get; set; } = string.Empty;

        public bool Success { get; set; } = true;

        public T Data { get; set; }
    }
}
