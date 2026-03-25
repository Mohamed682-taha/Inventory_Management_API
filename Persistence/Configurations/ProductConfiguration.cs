using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Supplier).HasMaxLength(100);
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.UpdatedAt).HasComputedColumnSql("GETDATE()");
            //Relationships
            builder.HasMany(p => p.Transactions)
                   .WithOne(t => t.Product)
                   .HasForeignKey(t => t.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.LowStockAlerts)
                   .WithOne(l => l.Product)
                   .HasForeignKey(l => l.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
