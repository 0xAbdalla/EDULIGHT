using EDULIGHT.Entities.ContentData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EDULIGHT.Configrations
{
    public class CourseConfigrations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500).IsRequired();
            builder.Property(p=>p.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.Level).IsRequired();
            builder.Property(p => p.WelcomeMessage).HasMaxLength(100).IsRequired();
            builder.Property(p => p.CongratulationMessage).HasMaxLength(100).IsRequired();
            builder.Property(p=>p.Duration).IsRequired();
            builder.Property(l=>l.Level).HasConversion(l=>l.ToString(),l=>(Level)Enum.Parse(typeof(Level), l));
            builder.Property(l => l.Language).HasConversion(l => l.ToString(), l => (Language)Enum.Parse(typeof(Language), l));

        }
    }
}
