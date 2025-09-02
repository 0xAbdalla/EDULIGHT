using EDULIGHT.Entities.Enroll_Pay;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDULIGHT.Configrations
{
    public class CartConfigrations : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            //builder.HasMany(p=>p.Courses).WithOne(p=>p.).HasForeignKey(p => p.CartId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
