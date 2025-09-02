using EDULIGHT.Entities.ContentData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDULIGHT.Configrations
{
    public class ContentCourseConfigrations : IEntityTypeConfiguration<ContentCourse>
    {
        public void Configure(EntityTypeBuilder<ContentCourse> builder)
        {
            builder.Property(p => p.ContentTitle).HasMaxLength(50).IsRequired();
            builder.Property(p=>p.ContentType).IsRequired();
            builder.Property(t => t.ContentType).HasConversion(t => t.ToString(), t => (ContentType)Enum.Parse(typeof(ContentType),t));

        }
    }
}
