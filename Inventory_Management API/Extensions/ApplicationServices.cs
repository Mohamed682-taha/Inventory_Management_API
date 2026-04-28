using Domain.Interfaces;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;

namespace Inventory_Management_API.Extensions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddingApplicationServices(this IServiceCollection Services)
        {
            Services.AddTransient<IMailService,MailService>();
            Services.AddScoped<ITokenService,TokenService>();
            Services.AddScoped<ILowStockService,LowStockService>();
            Services.AddAutoMapper(p => p.AddProfiles([new ProductProfile(),new CategoriesProfile(),new TransactionProfile()]));
            return Services;
        }
    }
}
