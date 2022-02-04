var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IWeatherForecastConfigService, WeatherForecastConfigService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    { Title = builder.Environment.ApplicationName, Version = "v1" });
});

await using var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Environment.ApplicationName} v1"));
}

app.UseHttpsRedirection();
app.UseRouting();

var Summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (IWeatherForecastConfigService _weatherForecastConfigService) =>
{
    var numberOfDays = _weatherForecastConfigService.NumberOfDays();
    if (numberOfDays <= 0)
    {
        return Results.BadRequest();
    }

    var forecast = Enumerable.Range(1, numberOfDays)
        .Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    return Results.Ok(forecast);
});

await app.RunAsync();

public partial class Program { }
