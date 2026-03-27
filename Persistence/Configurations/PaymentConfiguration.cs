using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(p => p.Amount).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PaymentMethod).HasConversion(pm => pm.ToString(),pm => (PaymentMethod)Enum.Parse(typeof(PaymentMethod),pm));
            builder.Property(p => p.PaymentStatus).HasConversion(ps => ps.ToString(),ps => (PaymentStatus)Enum.Parse(typeof(PaymentStatus),ps));
            builder.HasOne(p => p.Transaction)
                   .WithOne(t => t.Payment)
                   .HasForeignKey<Payment>(p => p.TransactionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
