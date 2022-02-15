using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Powerplant.Infra.CrossCutting.ExtensionsMethods;
using Serilog.Context;

namespace Powerplant.Api.Middleware
{
    /// <summary>
    /// Include Correlation-Id in Logs
    /// </summary>
    public class CorrelationLoggerMiddleware : CorrelationIdBase
    {
        public CorrelationLoggerMiddleware(RequestDelegate next) : base(next)
        { }

        public override async Task InvokeAsync(HttpContext httpContext)
        {
            if (!httpContext.IsWebsocket())
            {
                httpContext.Request.Headers.TryGetValue(KEY, out StringValues correlationId);

                using (LogContext.PushProperty(PROPERTY, correlationId.FirstOrDefault()))
                {
                    await _next.Invoke(httpContext);
                }
            }
            else
            {
                await _next.Invoke(httpContext);
            }
        }
    }
}
