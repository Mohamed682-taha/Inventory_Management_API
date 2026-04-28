using Microsoft.AspNetCore.Mvc;
using Presentation.Errors;

namespace Inventory_Management_API.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddingConfigureServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value!.Errors.Count() > 0)
                                            .SelectMany(m => m.Value!.Errors)
                                            .Select(e => e.ErrorMessage);
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });
            return Services;
        }
    }
}
