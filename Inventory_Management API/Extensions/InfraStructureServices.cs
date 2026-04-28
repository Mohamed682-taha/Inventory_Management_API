using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;
using Persistence.Repositories;
using Service;
using ServiceAbstraction;

namespace Inventory_Management_API.Extensions
{
    public static class InfraStructureServices
    {
        public static IServiceCollection AddingInfraStructureServices(this IServiceCollection Services,IConfiguration Configuration)
        {
            Services.AddDbContext<InventoryDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            Services.AddScoped<IUnitOfWork,UnitOfWork>();
            Services.AddScoped<IServiceManager,ServiceManager>();
            Services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            return Services;
        }
    }
}
