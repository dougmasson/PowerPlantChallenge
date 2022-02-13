using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Powerplant.Api.Middleware
{
    /// <summary>
    /// Include Correlation-Id into Header of Response
    /// </summary>
    public class CorrelationIdResponseMiddleware : CorrelationIdBase
    {
        public CorrelationIdResponseMiddleware(RequestDelegate next) : base(next)
        { }

        public override async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Add(KEY, httpContext.Request.Headers.First(x => x.Key.Equals(KEY)).Value);

                return Task.CompletedTask;
            }, context);

            await _next(context);
        }
    }
}
