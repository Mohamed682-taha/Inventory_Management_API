
using Domain.Interfaces;
using Domain.Models;
using Inventory_Management_API.Extensions;
using Inventory_Management_API.MiddleWares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.Data;
using Persistence.Data.DbContexts;
using Persistence.Repositories;
using Presentation.Errors;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using System.Text;

namespace Inventory_Management_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddingSwaggerServices();
            builder.Services.AddingInfraStructureServices(builder.Configuration);
            builder.Services.AddingApplicationServices();
            builder.Services.AddingConfigureServices();
            builder.Services.AddingIdentityServices(builder.Configuration);

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var _dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();
            var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            try
            {
                await _dbContext.Database.MigrateAsync();
                await DataSeed.SeedDataAsync(_dbContext,_roleManager);
            }
            catch ( Exception ex )
            {
                logger.LogError(ex,"An error occured while migrating/seeding");
            }

            app.UseMiddleware<CustomExceptionMiddleware>();
            if ( app.Environment.IsDevelopment() )
            {
                app.MapOpenApi();
            }


            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
