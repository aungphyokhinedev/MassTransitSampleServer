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
        public async Task<AplusListResponse> GetData(int page, int pageSize)
        {
            var result = await _db.GetListAsync(new AplusListRequest(tables:"users", pageSize: pageSize, page: page){
            });
           
            return result;
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

public class User{
    public string NRC {get;set;}
    public string Mobile_no {get;set;}
    public string Email {get;set;}
    public string PIN {get;set;}
    public string UserType {get;set;}
    public string Referer {get;set;}

    public string Status {get;set;}
    public string DeviceId {get;set;}

    public bool DeleteFlag{get;set;}
}