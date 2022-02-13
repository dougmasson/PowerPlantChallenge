using Murmur;
using Powerplant.API.Logs.Configurations;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Text;

namespace Powerplant.API.Logs
{
    /// <summary>
    /// Create a uniquely identify for logs
    /// </summary>
    public class EventTypeEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var murmur = MurmurHash.Create32();
            var bytes = Encoding.UTF8.GetBytes(logEvent.MessageTemplate.Text);
            var hash = murmur.ComputeHash(bytes);
            var numericHash = BitConverter.ToUInt32(hash, 0);

            var eventType = propertyFactory.CreateProperty(SerilogConfig.PROPERTY_EVENT_TYPE, numericHash);
            logEvent.AddPropertyIfAbsent(eventType);
        }
    }
}
