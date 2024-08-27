using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Netflix.Application.Cartoons.Common;
using Netflix.Application.Cartoons.Queries.GetAllCartoons;
using Netflix.Application.Common.Behaviors;
using Netflix.Application.Common.Content;
using System.Reflection;

namespace Netflix.Application.Extensions
{
    public static class DependencyInjection
    {
        private static readonly Assembly ApplicationAssembly = typeof(DependencyInjection).Assembly;
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            //inject dependencies from application project

            //services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            //services.AddScoped<IFilmService, FilmService>();

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(ApplicationAssembly);
            });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            //services.AddScoped<
            //    IPipelineBehavior<RegisterCommand, AuthenticationResult>,
            //    ValidateRegisterCommandBehavior>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}
