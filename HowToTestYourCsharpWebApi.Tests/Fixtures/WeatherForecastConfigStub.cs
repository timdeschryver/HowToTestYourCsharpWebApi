using HowToTestYourCsharpWebApi.Api.Ports;

namespace HowToTestYourCsharpWebApi.Tests.Fixtures
{
    public class WeatherForecastConfigStub : IWeatherForecastConfigService
    {
        public int NumberOfDays() => 7;
    }
}
