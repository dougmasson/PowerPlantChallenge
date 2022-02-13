using System;
using System.Linq;
using Powerplant.Api.Configurations;
using Serilog;
using Serilog.Core;

namespace Powerplant.API.Logs.Configurations
{
    /// <summary>
    /// Configure Serilog
    /// </summary>
    public static class SerilogConfig
    {
        public static readonly string PROPERTY_EVENT_TYPE = "EventType";

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
