using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;

namespace Powerplant.Api.Configurations
{
    public static partial class ServiceExtensionsConfigurations
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerExamplesFromAssemblies(Assembly.LoadFrom(Path.Combine(AppContext.BaseDirectory, "Powerplant.Infra.Mock.dll")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Powerplant.API", Version = "v1" });

                c.ExampleFilters();
            });

            return services;
        }

        public static IApplicationBuilder AddSwaggerApp(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Powerplant.API v1");

                c.DisplayRequestDuration();

                c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
            });

            return app;
        }

    }
}
