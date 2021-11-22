using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HAApi.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception e)
            {
                _logger.LogWarning("Exception occured. Name: {name}, Message: {message}", e.GetType().Name, e.Message);

                HandleException(httpContext.Response, e);
            }
        }

        private static void HandleException(HttpResponse httpResponse, Exception exception)
        {            
            httpResponse.Headers.Add("Exception-Type", exception.GetType().Name);
            httpResponse.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = exception.Message;
            httpResponse.StatusCode = (int)HttpStatusCode.BadRequest;

            //await httpResponse.WriteAsync(exception.Message).ConfigureAwait(false);
        }
    }

    public static class AppBuilderExtenstions
    {
        public static void UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
