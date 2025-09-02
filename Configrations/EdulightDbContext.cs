using EDULIGHT.Entities;
using EDULIGHT.Entities.ContentData;
using EDULIGHT.Entities.Enroll_Pay;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EDULIGHT.Configrations
{
    public class EdulightDbContext : DbContext
    {
        public EdulightDbContext(DbContextOptions<EdulightDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        //public DbSet<Student> Students { get; set; }
        //public DbSet<Instructor> Instructors { get; set; }
        //public DbSet<Companies> Companies { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Sections> Sections { get; set; }

        public DbSet<ContentCourse> ContentCourses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Roadmap> Roadmaps { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Review> Review { get; set; }




    }
}
