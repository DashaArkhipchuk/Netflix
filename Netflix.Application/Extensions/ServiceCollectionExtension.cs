using Microsoft.Extensions.DependencyInjection;
using Netflix.API.Controllers;
using Netflix.Application.IServices;
using Netflix.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            //inject dependencies from application project

            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<IFilmService, FilmService>();

            return services;
        }
    }
}
