using Powerplant.Api.Configurations;
using Powerplant.Infra.CrossCutting.Logs;
using Serilog;
using Serilog.Core;
using System;
using System.Linq;

namespace Powerplant.API.Logs.Configurations
{
    /// <summary>
    /// Configure Serilog
    /// </summary>
    public static class SerilogConfig
    {
        public static Logger CreateLogger()
        {
            Environment.SetEnvironmentVariable("BASEDIR", AppContext.BaseDirectory);

            return new LoggerConfiguration()
                    .ReadFrom.Configuration(ReadAppSettingsJsonConfig.LoadAppConfiguration())
                    .Enrich.With<EventTypeEnricher>()
                    .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger")))
                    .CreateLogger();
        }
    }
}
