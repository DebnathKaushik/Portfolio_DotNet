using Entity.General_Entity;
using Microsoft.EntityFrameworkCore;

namespace Entity
{
    public class DB_Context : DbContext
    {
        // Constructor to configure connection string
        public DB_Context(DbContextOptions<DB_Context> options) : base(options)
        {
        }

        // DbSets for your tables
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Education> Educations { get; set; }


        // To enable Cascade Delete -----------------------------------------------------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // USER → PROJECT : Cascade Delete
            modelBuilder.Entity<Project>()
                .HasOne<User>()
                .WithMany(u => u.Projects)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // USER → EXPERIENCE : Cascade Delete
            modelBuilder.Entity<Experience>()
                .HasOne<User>()
                .WithMany(u => u.Experiences)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // USER → EDUCATION : Cascade Delete
            modelBuilder.Entity<Education>()
                .HasOne<User>()
                .WithMany(u => u.Educations)
                .HasForeignKey(ed => ed.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}