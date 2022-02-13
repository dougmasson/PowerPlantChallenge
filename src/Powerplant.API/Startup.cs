using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Powerplant.Api.Configurations;
using Powerplant.Infra.DependencyInjection.Config;

namespace Powerplant.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioningOptions();

            services.AddResponseCompressionOptions();

            services.AddResponseCompressionOptions();

            services.AddCorsOptions();

            services.AddRouting(ConfigureOptions.RouteOptions());

            services.AddIoC();

            services.AddControllers()
                .AddFluentValidation(ConfigureOptions.FluentValidationMvcOptions())
                .AddJsonOptions(ConfigureOptions.JsonOptions());

            services.ConfigureFluentValidation();

            services.AddDI_FluentValidation();

            services.AddSwaggerService();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.AddSwaggerApp();
            }

            app.UseCustomMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
