using Presentation.Errors;

namespace Inventory_Management_API.MiddleWares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next,ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                await NotFoundEndpointHandler(context);
            }
            catch ( Exception ex )
            {
                _logger.LogError(ex.Message);
                context.Response.StatusCode = 500;
                var response = new ApiServerErrorResponse(500,ex.Message,ex.StackTrace);
                await context.Response.WriteAsJsonAsync(response);
            }
        }

        private static async Task NotFoundEndpointHandler(HttpContext context)
        {
            if ( context.Response.StatusCode == StatusCodes.Status404NotFound )
            {
                var response = new ApiResponse(StatusCodes.Status404NotFound,$"Endpoint with Url:{context.Request.Path} is not found");
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
