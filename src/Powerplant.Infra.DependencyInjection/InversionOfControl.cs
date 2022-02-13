using Microsoft.Extensions.DependencyInjection;
using Powerplant.Core.Domain.Interface;
using Powerplant.Core.Service;
using Powerplant.Core.Service.Factory;

namespace Powerplant.Infra.DependencyInjection.Config
{
    public static class InversionOfControl
    {
        public static void AddIoC(this IServiceCollection services)
        {
            services.AddScoped<IProductionPlanService, ProductionPlanService>();
            services.AddSingleton<IPowerPlanFactory, PowerPlanFactory>();
        }
    }
}
