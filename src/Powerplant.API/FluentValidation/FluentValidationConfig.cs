using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Powerplant.Core.Domain.Model.System;
using System.Collections.Generic;
using System.Linq;

namespace Powerplant.Api.Configurations
{
    public static partial class ServiceExtensionsConfigurations
    {
        /// <summary>
        /// Check Validation automatic with Fluent Validation
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = c =>
                {
                    var invalidItems = c.ModelState.Where(ms => ms.Value.Errors.Any());

                    var errorDetails = new List<ErrorDetail>();

                    foreach (var item in invalidItems)
                    {
                        var erroDetail = new ErrorDetail()
                        {
                            Field = item.Key,
                            Error = item.Value.Errors[0].ErrorMessage
                        };

                        errorDetails.Add(erroDetail);
                    }

                    return new BadRequestObjectResult(errorDetails);
                };
            });
        }
    }
}
