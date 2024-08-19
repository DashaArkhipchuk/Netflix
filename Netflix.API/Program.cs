using Netflix.API;
using Netflix.API.Middleware;
using Netflix.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPresentation().AddApplicationDI().AddInfrastructureDI(builder.Configuration);


var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapControllers();

//app.UseErrorHandlingMiddleware();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
