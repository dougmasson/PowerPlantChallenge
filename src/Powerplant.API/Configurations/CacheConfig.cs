using Microsoft.Extensions.DependencyInjection;

namespace Powerplant.Api.Configurations
{
    public static partial class ServiceExtensionsConfigurations
    {
        public static IServiceCollection AddCaching(this IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            return services;
        }
    }
}
