namespace Inventory_Management_API.Extensions
{
    public static class SwaggerServices
    {
        public static IServiceCollection AddingSwaggerServices(this IServiceCollection Services)
        {
            Services.AddOpenApi();
            return Services;
        }
    }
}
