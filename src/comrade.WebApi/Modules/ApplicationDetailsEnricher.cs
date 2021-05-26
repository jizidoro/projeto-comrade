using System.Reflection;
using Serilog.Core;
using Serilog.Events;

namespace comrade.WebApi.Modules
{
    public class ApplicationDetailsEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var applicationAssembly = Assembly.GetEntryAssembly();

            var name = applicationAssembly?.GetName().Name;
            var version = applicationAssembly?.GetName().Version;
            
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ApplicationName", name));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ApplicationVersion", version));
        }
    }
}