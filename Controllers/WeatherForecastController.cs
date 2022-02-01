using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IBus _bus;

        public WeatherForecastController(IBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var client = _bus.CreateRequestClient<GetWeatherForecasts>();

            var response = await client.GetResponse<WeatherForecasts>(new { });
            return response.Message.Forecasts;
        }
    }
}