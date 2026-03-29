using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.DbContexts;
using System.Text.Json;

namespace Persistence.Data
{
    public static class DataSeed
    {
        public static async Task SeedDataAsync(InventoryDbContext _dbContext)
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
            await _dbContext.SaveChangesAsync();
        }
    }
}
