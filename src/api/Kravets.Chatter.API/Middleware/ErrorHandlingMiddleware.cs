using Kravets.Chatter.API.Models.Errors;
using Kravets.Chatter.Common.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Kravets.Chatter.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApplicationSettings _appSettings;

        public ErrorHandlingMiddleware(
            RequestDelegate next, 
            IOptions<ApplicationSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
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

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            const HttpStatusCode code = HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(
                new ErrorResponse
                {
                    Message = ex.Message,
                    StackTrace = _appSettings.ShowStackTraceOnError ? ex.StackTrace : null
                });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
