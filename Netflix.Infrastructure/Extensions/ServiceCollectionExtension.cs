using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netflix.Domain.IRepository;
using Netflix.Infrastructure;
using Netflix.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            //inject dependencies from infrastructure project

            services.AddDbContext<NetflixProjectContext>(options=> options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IFilmRepository, FilmRepository>();

            return services;
        }
    }
}
