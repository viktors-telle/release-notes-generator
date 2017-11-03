using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ReleaseNotesGenerator.Attributes;
using ReleaseNotesGenerator.Exceptions;

namespace ReleaseNotesGenerator.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            var code = HttpStatusCode.InternalServerError;
            if (exception.GetType().IsSubclassOf(typeof(BaseException)))
            {
                code = exception.GetType().GetCustomAttribute<HttpStatusCodeAttribute>()?.HttpStatusCode ?? HttpStatusCode.InternalServerError;
            }

            await WriteExceptionAsync(context, exception, code).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception, HttpStatusCode code)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;

            var message = exception.InnerException?.Message ?? exception.Message;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                error = new
                {
                    message,
                    exception = exception.GetType().Name
                }
            })).ConfigureAwait(false);
        }
    }
}
