using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace EmployeeApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        static List<WeatherForecast> forecasts=new List<WeatherForecast>();
        private static readonly string[] Summaries = new[]
        {
          "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]            
            //})
            //.ToArray();

            return forecasts;
        }
        [HttpGet("sum")]
        public WeatherForecast Get([FromQuery] string str)
        {
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //}).Where(x => x.Summary == str).FirstOrDefault();

            WeatherForecast val= (from x in forecasts where x.Summary == str select x).FirstOrDefault();
            return (val);
        }
        //[HttpPost("api/weatherforecast/")]
        [HttpPost]
        public IEnumerable<WeatherForecast> post(WeatherForecast obj)
        {
            forecasts.Add(obj);
            return forecasts;
        }
        [HttpDelete]
        public IEnumerable<WeatherForecast> delete(string sum)
        {
            forecasts.RemoveAll(x => x.Summary == sum);
            return forecasts;
        }
        [HttpPut]
        public IEnumerable<WeatherForecast> put(string sum)
        {
            foreach(var x in forecasts)
            {
                if(x.Summary == sum)
                    x.Date= DateTime.Now;
            }
            return forecasts;
        }
    }
}