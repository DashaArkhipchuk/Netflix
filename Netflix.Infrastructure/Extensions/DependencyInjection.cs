
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Netflix.Application.Common.Services;
using Netflix.Application.Interfaces.Authentication;
using Netflix.Domain.IRepository;
using Netflix.Infrastructure;
using Netflix.Infrastructure.Authentication;
using Netflix.Infrastructure.Repositories;
using Netflix.Infrastructure.Services;
using System.Text;

namespace Netflix.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            //inject dependencies from infrastructure project

            services.AddAuth(configuration);

            services.AddDbContext<NetflixProjectContext>(options=> options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddScoped<ISeriesRepository, SeriesRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IContentByTypesRepository, ContentByTypesRepository>();

            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });

            return services;
        }

    }
}
