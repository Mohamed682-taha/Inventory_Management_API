using Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Persistence.Data.DbContexts;
using System.Text;

namespace Inventory_Management_API.Extensions
{
    public static class IdentityServices
    {
        public static IServiceCollection AddingIdentityServices(this IServiceCollection Services,IConfiguration Configuration)
        {
            Services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<InventoryDbContext>();
            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(Configuration["JWT:Key"]!))
                };
            });
            return Services;
        }
    }
}
