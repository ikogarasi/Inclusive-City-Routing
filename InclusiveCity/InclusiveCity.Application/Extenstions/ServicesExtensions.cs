using InclusiveCity.Application;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InclusiveCity.Infrastructure.Extenstions
{
    public static class ServicesExtensions
    {
        public static void AddMediatr(this IServiceCollection services) 
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
