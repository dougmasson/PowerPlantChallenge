using Microsoft.AspNetCore.Builder;
using Powerplant.Api.Middleware;

namespace Powerplant.Api.Configurations
{
    public static partial class ApplicationExtensionsConfigurations
    {
        /// <summary>
        /// Include custom Middlewares 
        /// </summary>
        /// <param name="app"></param>
        public static void UseCustomMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<CorrelationIdRequestMiddleware>();
            app.UseMiddleware<CorrelationLoggerMiddleware>();
            app.UseMiddleware<ResponseTimeMiddleware>();
            app.UseMiddleware<CorrelationIdResponseMiddleware>();
        }
    }
}
