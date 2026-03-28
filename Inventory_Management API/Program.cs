
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Data.DbContexts;
using Persistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;

namespace Inventory_Management_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddDbContext<InventoryDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IServiceManager,ServiceManager>();
            builder.Services.AddAutoMapper(p => p.AddProfile(new ProductProfile()));
            builder.Services.AddOpenApi();

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var _dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();
            try
            {
                await _dbContext.Database.MigrateAsync();
                await DataSeed.SeedDataAsync(_dbContext);
            }
            catch ( Exception ex )
            {
                logger.LogError(ex,"An error occured while migrating/seeding");
            }

            if ( app.Environment.IsDevelopment() )
            {
                app.MapOpenApi();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
