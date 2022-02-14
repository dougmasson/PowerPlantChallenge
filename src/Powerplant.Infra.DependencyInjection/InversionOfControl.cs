using Microsoft.Extensions.DependencyInjection;
using Powerplant.Core.Domain.Interface.Cache;
using Powerplant.Core.Domain.Interface.Infra.Repository;
using Powerplant.Core.Domain.Interface.Service;
using Powerplant.Core.Service;
using Powerplant.Core.Service.Factory;
using Powerplant.Infra.CrossCutting.Cache;
using Powerplant.Infra.Data.Repository;

namespace Powerplant.Infra.DependencyInjection.Config
{
    public static class InversionOfControl
    {
        public static void AddIoC(this IServiceCollection services)
        {
            services.AddScoped<IProductionPlanService, ProductionPlanService>();
            services.AddScoped<IParamRepository, ParamRepository>();
            services.AddSingleton<IPowerPlantFactory, PowerPlantFactory>();
            services.AddScoped<ICacheService, CacheService>();
        }
    }
}
