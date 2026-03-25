using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    class LowStockAlertConfiguration : IEntityTypeConfiguration<LowStockAlert>
    {
        public void Configure(EntityTypeBuilder<LowStockAlert> builder)
        {
            builder.Property(l => l.Threshold).HasDefaultValue(10);
            builder.Property(l => l.Date).HasDefaultValueSql("GETDATE()");
        }
    }
}
