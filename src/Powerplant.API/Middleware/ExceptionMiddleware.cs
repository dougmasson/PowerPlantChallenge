using Microsoft.AspNetCore.Http;
using Powerplant.Core.Domain.Model.System;
using Serilog;
using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Powerplant.Api.Middleware
{
    /// <summary>
    /// Handle Expecetion into Rest API
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Log.Error($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            ErrorDetail erroDetail;

            if (exception.InnerException is OperationCanceledException && ((OperationCanceledException)exception.InnerException) != null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                erroDetail = new ErrorDetail("408", HttpStatusCode.RequestTimeout.ToString());
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                erroDetail = new ErrorDetail("500", HttpStatusCode.InternalServerError.ToString());
            }

            return context.Response.WriteAsync(JsonSerializer.Serialize(erroDetail, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));
        }
    }
}
