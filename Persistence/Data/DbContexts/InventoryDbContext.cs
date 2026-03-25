using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Data.DbContexts
{
    public class InventoryDbContext : IdentityDbContext<AppUser,IdentityRole,string>
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<LowStockAlert> lowStockAlerts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
