using InclusiveCity.Application;
using InclusiveCity.Azure.BlobStorage;
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

        public static void AddAzureBlobStorage(this IServiceCollection services)
        {
            services.AddSingleton<IAzureStorage, AzureStorage>();
        }
    }
}
