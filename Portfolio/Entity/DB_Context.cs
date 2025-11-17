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

    }
}