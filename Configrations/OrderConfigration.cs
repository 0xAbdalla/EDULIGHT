using EDULIGHT.Entities;
using EDULIGHT.Entities.Enroll_Pay;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDULIGHT.Configrations
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(s=>s.SubTotal).HasColumnType("decimal(18,2)");
            builder.Property(s => s.Status).HasConversion(s => s.ToString(), s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s));

        }
    }
}
