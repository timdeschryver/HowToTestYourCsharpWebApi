using HowToTestYourCsharpWebApi.Api.Ports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace HowToTestYourCsharpWebApi.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastConfigService _weatherForecastConfigService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastConfigService weatherForecastConfigService)
        {
            _logger = logger;
            _weatherForecastConfigService = weatherForecastConfigService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var numberOfDays = _weatherForecastConfigService.NumberOfDays();
            if (numberOfDays <= 0)
            {
                return BadRequest();
            }

            var rng = new Random();
            return Ok(Enumerable.Range(1, numberOfDays).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray());
        }
    }
}
