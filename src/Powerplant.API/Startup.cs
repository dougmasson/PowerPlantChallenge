using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Powerplant.Api.Configurations;
using Powerplant.Infra.Data.Context;
using Powerplant.Infra.DependencyInjection.Config;
using Powerplant.Infra.WebsocketManager;

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

            services.AddWebSocketManagerServices();

            services.AddCaching();

            services.AddDbContext<ApiDbContext>(options => { options.UseInMemoryDatabase("productionPlan-api-in-memory"); });
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

            app.AddWebSocketManagerApp("/ws");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
