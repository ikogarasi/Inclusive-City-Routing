using InclusiveCity.Domain.Dto.Routes;
using InclusiveCity.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InclusiveCity.Infrastructure.Extenstions
{
    public static class ServicesExtensions
    {
        public static void AddGoogleMapsServices(this IServiceCollection services) 
        {
            services.AddHttpClient();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<IGoogleMapsService<RoutesApiRequestDto, RoutesApiResponseDto>, RoutesService>();
        }
    }
}
