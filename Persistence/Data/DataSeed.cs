using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Persistence.Data
{
    public static class DataSeed
    {
        public static async Task SeedDataAsync(InventoryDbContext _dbContext,RoleManager<IdentityRole> _roleManager)
        {
            if ( !await _dbContext.Categories.AnyAsync() )
            {
                using var CategoriesData = File.OpenRead(@"..\Persistence\Data\DataSeed\categories.json");
                var Categories = await JsonSerializer.DeserializeAsync<List<Category>>(CategoriesData);
                if ( Categories?.Count > 0 )
                    await _dbContext.Categories.AddRangeAsync(Categories);
            }
            if ( !await _dbContext.Products.AnyAsync() )
            {
                using var ProductsData = File.OpenRead(@"..\Persistence\Data\DataSeed\products.json");
                var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);
                if ( Products?.Count > 0 )
                    await _dbContext.Products.AddRangeAsync(Products);
            }
            if ( !await _dbContext.Roles.AnyAsync() )
            {
                string[] roles = ["Admin","Staff","Manager"];
                foreach ( var role in roles )
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(role));
                    if ( !result.Succeeded )
                    {
                        var errors = string.Join(",",result.Errors.Select(e => e.Description));
                        throw new Exception(errors);
                    }
                }

            }
            if ( !await _dbContext.Transactions.AnyAsync() )
            {
                using var transactionsData = File.OpenRead(@"..\Persistence\Data\DataSeed\transactions.json");
                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }, // For Type Property
                };
                var transactions = await JsonSerializer.DeserializeAsync<List<Transaction>>(transactionsData);
                if ( transactions?.Count > 0 )
                    await _dbContext.Transactions.AddRangeAsync(transactions);
            }
            await _dbContext.SaveChangesAsync();
        }

        public static async Task SeedUserWithAdminRole(UserManager<AppUser> _userManager,InventoryDbContext _dbContext)
        {
            var user = new AppUser()
            {
                Email = "mohamedtaha20@gmail.com",
                UserName = "mohamedtaha",
                PhoneNumber = "0123456789"
            };
            await _userManager.CreateAsync(user,"Pa$$w0rd");
            await _userManager.AddToRoleAsync(user,"Admin");

        }
    }
}

