using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Powerplant.Api.Configurations
{
    public static partial class ServiceExtensionsConfigurations
    {
        /// <summary>
        /// Configure Version of controllers
        /// </summary>
        /// <param name="services"></param>
        public static void AddApiVersioningOptions(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = false;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
