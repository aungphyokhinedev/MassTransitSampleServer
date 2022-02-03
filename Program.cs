using MassTransit;
using System.Reflection;
using WeatherAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAplusDataContext,AplusPostgresDataContext>();

builder.Services.AddMassTransit(x =>
            {
                //x.AddConsumers(Assembly.GetExecutingAssembly());


                x.AddConsumer<GetWeatherForecastConsumer>();
                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });


                x.AddRequestClient<GetWeatherForecasts>();

            }).AddMassTransitHostedService();




// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WeatherAPI", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherAPI v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
