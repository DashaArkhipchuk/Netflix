
using Azure.Storage.Blobs;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Netflix.Application.Common.Services;
using Netflix.Application.Interfaces.Authentication;
using Netflix.Domain.DTOs.NewsApi;
using Netflix.Domain.IRepository;
using Netflix.Domain.Services;
using Netflix.Infrastructure;
using Netflix.Infrastructure.Authentication;
using Netflix.Infrastructure.CloudStorage;
using Netflix.Infrastructure.ExternalApi.NewsApi;
using Netflix.Infrastructure.ExternalApi.Scraping;
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

            services.AddScoped<INewsRepository, NewsRepository>();

            services.AddScoped<ISocialMediaProfileRepository, SocialMediaProfileRepository>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<ICastingDirectorRepository, CastingDirectorRepository>();

            services.Configure<NewsApiOptions>(configuration.GetSection("NewsApi"));

            services.AddHttpClient<INewsApiHttpClientService, NewsApiHttpClientService>((sp, client) =>
            {
                var opts = sp.GetRequiredService<IOptions<NewsApiOptions>>().Value;
                client.BaseAddress = new Uri(opts.BaseUrl);
                client.DefaultRequestHeaders.Add("X-Api-Key", opts.ApiKey);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("LocalhostNetflixNewsIngestor/1.0");
            });

            services.AddHttpClient<IArticleContentScraperService, ArticleContentScraperService>();

            services.AddScoped<INewsApiExternalVendorRepository, NewsApiExternalVendorRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddSingleton<INewsPopulationSettings, NewsPopulationSettings>();

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddCloudStorage(configuration);

            services.AddHttpClient<NewsApiHttpClientService>();

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
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ClockSkew = TimeSpan.Zero
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

        public static IServiceCollection AddCloudStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var cloudinarySettings = new CloudinarySettings();
            configuration.Bind(CloudinarySettings.SectionName, cloudinarySettings);

            var cloudinary = new Cloudinary(new Account(cloudinarySettings.CloudName, cloudinarySettings.APIKey, cloudinarySettings.APISecret));
            cloudinary.Api.Timeout = cloudinarySettings.Timeout;
            cloudinary.Api.ChunkSize = cloudinarySettings.ChunkSize;

            services.AddSingleton(cloudinary);
            services.AddSingleton<ICloudStorageService, CloudinaryCloudStorageService>();
            return services;
        }
    }
}
