using CompanyEmployees.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployees.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique();
            });
        }
    }
}