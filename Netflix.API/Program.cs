using Netflix.API;
using Netflix.API.Middleware;
using Netflix.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPresentation().AddApplicationDI().AddInfrastructureDI(builder.Configuration);

builder.Services.AddCors(options =>
                options.AddPolicy(
                    "CorsPolicy",
                    policy =>
                        policy
                            .WithOrigins("https://localhost:3000/", "http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                            )
                );


var app = builder.Build();


// Configure the HTTP request pipeline.
app.MapControllers();

//app.UseErrorHandlingMiddleware();

app.UseSwagger();
app.UseSwaggerUI();

app.UseErrorHandlingMiddleware();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
