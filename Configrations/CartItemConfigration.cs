using EDULIGHT.Entities.ContentData;
using EDULIGHT.Entities.Enroll_Pay;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDULIGHT.Configrations
{
    public class CartItemConfigration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasOne(x => x.Course).WithMany().HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Cart).WithMany(p => p.items).HasForeignKey(x => x.CartId).OnDelete(DeleteBehavior.Cascade);


        }
    }
}
