using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Powerplant.Api.Configurations
{
    public static partial class ConfigureOptions
    {
        /// <summary>
        /// System.Text.Json options
        /// </summary>
        /// <returns></returns>
        public static Action<JsonOptions> JsonOptions() => options =>
        {
            options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        };
    }
}
