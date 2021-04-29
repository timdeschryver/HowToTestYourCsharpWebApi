using FluentAssertions;
using HowToTestYourCsharpWebApi.Api;
using HowToTestYourCsharpWebApi.Api.Ports;
using HowToTestYourCsharpWebApi.Tests.Fixtures;
using HowToTestYourCsharpWebApi.Tests.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace HowToTestYourCsharpWebApi.Tests
{
    public class WeatherForecastControllerTests : IntegrationTest
    {
        public WeatherForecastControllerTests(ApiWebApplicationFactory fixture)
          : base(fixture) { }

        [Fact]
        public async Task Get_Should_Return_Forecast()
        {
            var forecast = await _client.GetAndDeserialize<WeatherForecast[]>("/weatherforecast");
            forecast.Should().HaveCount(7);
        }

        [Fact]
        public async Task Get_Should_ResultInABadRequest_When_ConfigIsInvalid()
        {
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddTransient<IWeatherForecastConfigService, InvalidWeatherForecastConfigStub>();
                });
            })
            .CreateClient();

            var response = await client.GetAsync("/weatherforecast");
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        public class InvalidWeatherForecastConfigStub : IWeatherForecastConfigService
        {
            public int NumberOfDays() => -3;
        }
    }
}
