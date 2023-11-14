using System.Reflection;
using SearchAPI.Application.Contracts;
using SearchAPI.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SearchAPI.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IRectangleService, RectangleService>();


            return services;
        }
    }
}
