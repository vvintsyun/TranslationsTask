using Newtonsoft.Json;
using System.Net;

namespace TranslationsTask.Helpers
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

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = ex switch
            {
                UserFriendlyException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError,
            };

            var result = JsonConvert.SerializeObject(new
            {
                errorMessage = ex.Message,
                errorType = ex.GetType().ToString(),
                traceIdentifier = context.TraceIdentifier,
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
