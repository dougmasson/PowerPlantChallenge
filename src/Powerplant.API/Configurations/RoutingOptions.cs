using System;
using Microsoft.AspNetCore.Routing;

namespace Powerplant.Api.Configurations
{
    public static partial class ConfigureOptions
    {
        /// <summary>
        /// Make routes globally lowercase
        /// </summary>
        /// <returns></returns>
        public static Action<RouteOptions> RouteOptions() => options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        };

    }
}
