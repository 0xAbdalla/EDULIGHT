using EDULIGHT.Entities.IdentityUsers;
using EDULIGHT.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EDULIGHT.Identity.Context
{
    public class EdulightIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public EdulightIdentityDbContext(DbContextOptions<EdulightIdentityDbContext> options) : base(options)
        {
            
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Companies>().HasKey(p=> new {p.Id,p.CompanyId});
        //}
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Companies> Companies { get; set; }
    }
}
