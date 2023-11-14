using SearchAPI.Domain.Contracts;
using SearchAPI.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAPI.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddNpgsql<SearchAPIDbContext>(connectionString: configuration.GetConnectionString("SearchAPIConnectionString"));

            services.AddScoped<IRectangleRepository, RectangleRepository>();

            return services;
        }
    }
}
