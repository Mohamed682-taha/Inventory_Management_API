using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(t => t.Date).HasDefaultValueSql("GETDATE()");
            builder.Property(t => t.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(t => t.Type).HasConversion(t => t.ToString(),t => (TransactionType)Enum.Parse(typeof(TransactionType),t));
            builder.HasOne(t => t.AppUser)
                   .WithMany(u => u.Transactions)
                   .HasForeignKey(t => t.AppUserId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
