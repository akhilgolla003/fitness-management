using Microsoft.EntityFrameworkCore;

namespace FitnessCenterManagement.Models
{
    public class FitnessCenterDbContext : DbContext
    {
        public FitnessCenterDbContext(DbContextOptions<FitnessCenterDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Attendance> Attendance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Class>().ToTable("Classes");
            modelBuilder.Entity<Class>().HasKey(c => c.ClassID);
            // Other configurations...

        }
    }
}
