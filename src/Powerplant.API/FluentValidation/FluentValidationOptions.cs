using FluentValidation.AspNetCore;
using Powerplant.API;
using System;

namespace Powerplant.Api.Configurations
{
    public static partial class ConfigureOptions
    {
        /// <summary>
        /// Fluent Validation Options
        /// </summary>
        /// <returns></returns>
        public static Action<FluentValidationMvcConfiguration> FluentValidationMvcOptions() => options =>
        {
            options.RegisterValidatorsFromAssemblyContaining<Startup>();
            options.DisableDataAnnotationsValidation = true;
        };

    }
}
