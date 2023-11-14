using System.Text.Json;

namespace SearchAPI.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await this.next.Invoke(httpContext);
            }
            catch (Exception error)
            {
               //TODO your error handler logic
            }
        }
    }
}

