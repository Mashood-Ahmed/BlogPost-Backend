using System.Net;
using System.Text.Json;

namespace Portfolio.API.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context )
        {
            try
            {
                await _next(context);
            }
            catch (Exception e) 
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch(e)
                {
                    case AppExceptions error:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException error:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = e?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
