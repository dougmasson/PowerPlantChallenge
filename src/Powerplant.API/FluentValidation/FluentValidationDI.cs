using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Powerplant.Core.Domain.Model.Input;
using Powerplant.Core.Domain.Validator;

namespace Powerplant.Api.Configurations
{
    public static partial class ServiceExtensionsConfigurations
    {
        public static void AddDI_FluentValidation(this IServiceCollection services)
        {
            services.AddTransient<IValidator<FuelsInput>, FuelsInputValidator>();
            services.AddTransient<IValidator<PowerPlantInput>, PowerPlantInputValidator>();
            services.AddTransient<IValidator<ProductionPlanInputDTO>, ProductionPlanInputDTOValidator>();
        }
    }
}
