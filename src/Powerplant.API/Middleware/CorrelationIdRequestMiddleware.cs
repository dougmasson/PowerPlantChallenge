using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Powerplant.Api.Middleware
{
    /// <summary>
    /// Create Correlation-Id in Request for Logs and Response
    /// </summary>
    public class CorrelationIdRequestMiddleware : CorrelationIdBase
    {
        public CorrelationIdRequestMiddleware(RequestDelegate next) : base(next)
        { }

        public override async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.ContainsKey(KEY) == false)
            {
                httpContext.Request.Headers.Add(KEY, Guid.NewGuid().ToString());
            }

            await _next(httpContext);
        }
    }
}
