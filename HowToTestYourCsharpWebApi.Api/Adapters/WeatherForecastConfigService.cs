using HowToTestYourCsharpWebApi.Api.Ports;

namespace HowToTestYourCsharpWebApi.Api.Adapters
{
    public class WeatherForecastConfigService : IWeatherForecastConfigService
    {
        public int NumberOfDays() => 7;
    }
}
