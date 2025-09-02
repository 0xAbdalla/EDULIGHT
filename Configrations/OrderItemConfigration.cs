using EDULIGHT.Entities.Enroll_Pay;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDULIGHT.Configrations
{
    public class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(p => p.Course, p => p.WithOwner());
            builder.Property(p=>p.Price).HasColumnType("decimal(18,2)");
        }
    }
}
