using System.Net;

namespace NZWalk.API.Middleware
{
    public class ExceptionHandler
    {
        private readonly ILogger logger;
        private readonly RequestDelegate next;

        public ExceptionHandler(ILogger<ExceptionHandler> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                //var errorId = Guid.NewGuid();

                logger.LogError(ex, ex.Message);

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    ErrorMessage = "Something Went Wrong!"
                };

            await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
