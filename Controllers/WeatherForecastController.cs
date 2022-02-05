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


        //http://localhost:5033/weatherForecast/GetList?PageSize=15&page=1
        [Route("GetList")]
        public async Task<AplusListResponse> GetList(int page, int pageSize)
        {
            var result = await _db.GetListAsync(new AplusListRequest(tables:"users", pageSize: pageSize, page: page){
                fields = "id,nrc,mobile_no",
                orderBy = "id desc",
                condition = new AplusWhereClause{
                    where = "id = @id",
                    parameters = new {
                        id = 4
                    },
                }
                
               

            });
           
            return result;
        }

        //http://localhost:5033/weatherForecast/adddata
        [Route("AddData")]
        public async Task<AplusDataResponse> Add()
        {
            var result = await _db.AddAsync(new users{
                   UID = 7,
                   NRC = "12/pzt(N)9400449",
                   PIN = "12120",
                   Mobile_no = "9956000",
                   Email= "old@gmail.com",
                  CreatedAt = DateTime.Now,
                   DeleteFlag = false
               }
            );
           
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

public class users{
    public int UID {get;set;}
    public string NRC {get;set;}
    public string Mobile_no {get;set;}
    public string Email {get;set;}
    public string PIN {get;set;}
    public string UserType {get;set;}
    public string Referer {get;set;}
    
    public string Status {get;set;}
    public string DeviceId {get;set;}

    public DateTimeOffset CreatedAt {get;set;}
    public bool DeleteFlag{get;set;}
}