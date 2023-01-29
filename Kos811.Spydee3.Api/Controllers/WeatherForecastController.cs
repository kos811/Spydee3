using Kos811.Spydee3.Api.Contract.Models;
using Kos811.Spydee3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kos811.Spydee3.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] s_summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly StubService _stubService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, StubService stubService)
    {
        _logger = logger;
        _stubService = stubService;
    }

    [HttpGet("GetWeatherForecast", Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogCritical("GetWeatherForecast called");
        return Enumerable.Range(1, 5).Select(
                index => new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = s_summaries[Random.Shared.Next(s_summaries.Length)]
                })
            .ToArray();
    }

    [HttpPost("GetAnswer")]
    public IActionResult GetAnswer(string question = "kek")
    {
        var answer = _stubService.GetAnswer(question);
        _logger.LogCritical("The answer for question '{0}' is {1}", question, answer);

        return Ok(string.Format("the answer is {0}", answer));
    }
}
