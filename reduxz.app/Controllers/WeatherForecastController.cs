using Domain.Entity;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace reduxz.app.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IShortLinkService _shortLinkService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IShortLinkService shortLinkService)
        {
            _logger = logger;
            _shortLinkService = shortLinkService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var saw = new ShortLinkModels
            {
                Link = new Uri("https://www.mongodb.com/try/download/community"),
                ShortLink = new Uri("https://www.mongodb.com/try/download/community"),
                Status = false,
                Validity = DateTime.Now,
                Title = "TESTE",
                CreatedAt = DateTime.Now
            };

            _shortLinkService.InsertShortLink(saw);

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
