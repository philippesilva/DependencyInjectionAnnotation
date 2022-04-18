namespace Example.Services
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> Get();
    }
}
