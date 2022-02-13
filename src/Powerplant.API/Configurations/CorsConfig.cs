using Microsoft.Extensions.DependencyInjection;

namespace Powerplant.Api.Configurations
{
    public static partial class ServiceExtensionsConfigurations
    {
        /// <summary>
        /// Cors Options
        /// </summary>
        /// <param name="services"></param>
        public static void AddCorsOptions(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }
    }
}
