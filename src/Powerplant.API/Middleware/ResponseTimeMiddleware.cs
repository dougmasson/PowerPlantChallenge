using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Powerplant.Api.Middleware
{
    /// <summary>
    /// Calculate time to execute request and add value into Header of Response
    /// </summary>
    public class ResponseTimeMiddleware
    {
        private const string KEY = "X-Response-Time-ms";
        private readonly RequestDelegate _next;

        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var watch = new Stopwatch();
            watch.Start();

            context.Response.OnStarting(state =>
            {
                watch.Stop();
                var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;

                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Add(KEY, new[] { responseTimeForCompleteRequest.ToString() });

                return Task.CompletedTask;
            }, context);

            await _next(context);
        }
    }
}
