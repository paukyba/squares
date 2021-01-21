using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Squares.Data.DAL;
using Squares.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Squares.Api.Controllers
{
    /// <summary>
    /// Weather forecast controler.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ApiContext apiContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ApiContext apiContext)
        {
            _logger = logger;
            this.apiContext = apiContext;
        }

        /// <summary>
        /// Gets weahter forecast
        /// </summary>
        /// <returns>WeatherForecast</returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            this._logger.LogError("Susigadino");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
