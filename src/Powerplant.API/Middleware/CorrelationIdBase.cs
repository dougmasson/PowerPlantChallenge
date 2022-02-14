namespace Powerplant.Api.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Classe base to implement Correlation-Id for Logs and Trace
    /// </summary>
    public class CorrelationIdBase
    {
        public static readonly string KEY = "X-Correlation-Id";
        protected const string PROPERTY = "corellationId";

        protected readonly RequestDelegate _next;

        public CorrelationIdBase(RequestDelegate next)
        {
            _next = next;
        }

        public virtual Task InvokeAsync(HttpContext httpContext) => throw new NotImplementedException();
    }
}
