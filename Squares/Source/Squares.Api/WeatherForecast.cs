using System;

namespace Squares.Api
{
    public class WeatherForecast
    {
        /// <summary>
        /// Date for the weather
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Temperature in C
        /// </summary>
        public int TemperatureC { get; set; }
        
        /// <summary>
        /// Temperature in F
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Summart 
        /// </summary>
        public string Summary { get; set; }
    }
}
