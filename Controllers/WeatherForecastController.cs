using MassTransit;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Npgsql; 

namespace WeatherAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IBus _bus;
        private IAplusDataContext _db;

        public WeatherForecastController(IBus bus, IAplusDataContext db)
        {
            _bus = bus;
            _db = db;
        }

        [Route("TestDb")]
        public async Task<IEnumerable<string>> GetData()
        {
            var result = await _db.GetListAsync(new AplusListRequest(tables:"users", pageSize: 1, page: 1){
            });
           
            return new List<string>();
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