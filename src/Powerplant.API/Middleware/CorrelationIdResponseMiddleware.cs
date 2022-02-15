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

                var header = httpContext.Request.Headers.FirstOrDefault(x => x.Key.Equals(KEY));

                if (header.Key != null)
                {
                    httpContext.Response.Headers.Add(KEY, header.Value);
                }

                return Task.CompletedTask;
            }, context);

            await _next(context);
        }
    }
}
