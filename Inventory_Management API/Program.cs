
using Domain.Interfaces;
using Domain.Models;
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
            builder.Services.AddDbContext<InventoryDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IServiceManager,ServiceManager>();
            builder.Services.AddAutoMapper(p => p.AddProfile(new ProductProfile()));
            builder.Services.AddOpenApi();

            builder.Services.Configure<ApiBehaviorOptions>((options) =>
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

            builder.Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<InventoryDbContext>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
                };
            });
            builder.Services.AddScoped<ITokenService,TokenService>();
            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var _dbContext = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger<Program>();
            var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
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
