using InclusiveCity.Domain.Interfaces.Services;
using InclusiveCity.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InclusiveCity.Infrastructure.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddInfrastractureServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IOsrmService, OsrmService>();
            services.AddScoped<IOverpassApiService, OverpassApiService>();
        }
    }
}
