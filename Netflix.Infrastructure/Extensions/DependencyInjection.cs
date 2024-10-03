
using Azure.Storage.Blobs;
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

            services.AddDbContext<NetflixProjectContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddScoped<ISeriesRepository, SeriesRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IContentByTypesRepository, ContentByTypesRepository>();

            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IProjectTypeRepository, ProjectTypeRepository>();
            services.AddScoped<IRoleTypeRepository, RoleTypeRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IEthnicAppearanceRepository, EthnicAppearanceRepository>();

            services.AddScoped<ICastingDirectorTypeRepository, CastingDirectorTypeRepository>();
            services.AddScoped<ICastingCallRepository, CastingCallRepository>();
            services.AddScoped<ISubmissionRepository, SubmissionRepository>();
            services.AddScoped<IAuditionRepository, AuditionRepository>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<ICastingDirectorRepository, CastingDirectorRepository>();

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            var str = configuration["AzureStorageConnectionString"];
            services.AddSingleton(x => new BlobServiceClient(configuration["AzureStorageConnectionString"]));
            services.AddSingleton<ICloudStorageService, CloudStorageService>();

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

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Actor", p =>
                p.RequireClaim("isActor", "true"));

                options.AddPolicy("Director", p =>
                p.RequireClaim("isDirector", "true"));

            }
            );


            return services;
        }

    }
}
