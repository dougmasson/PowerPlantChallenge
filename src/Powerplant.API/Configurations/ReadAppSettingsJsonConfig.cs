using Microsoft.Extensions.Configuration;
using Powerplant.API;
using System;

namespace Powerplant.Api.Configurations
{
    public static class ReadAppSettingsJsonConfig
    {
        /// <summary>
        /// Read Aplication Json file and allow to reload properties
        /// </summary>
        /// <returns></returns>
        public static IConfigurationRoot LoadAppConfiguration()
        {
            return new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"hosting.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
               .AddUserSecrets<Startup>(true)
               .Build();
        }
    }
}
