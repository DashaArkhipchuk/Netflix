using Netflix.API;
using Netflix.API.Middleware;
using Netflix.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

<<<<<<< Updated upstream
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
=======
builder.Services.AddPresentation().AddApplicationDI().AddInfrastructureDI(builder.Configuration);
>>>>>>> Stashed changes


var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapControllers();

app.UseErrorHandlingMiddleware();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
