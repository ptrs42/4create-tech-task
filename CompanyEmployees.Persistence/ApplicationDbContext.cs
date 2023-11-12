using CompanyEmployees.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployees.Persistence
{
    /// <summary>
    /// Class <c>ApplicationDbContext</c> represents the database context used to access the
    /// database from the repositories.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// This constructor initializes an instance of the <c>ApplicationDbContext</c> class with
        /// the appropriate context options passed to the base class.
        /// </summary>
        /// <param name="options">
        /// <c>options</c> are the context options passed to the base class. this is needed to
        /// properly configure the connection string in case of Development/Production environment
        /// or to configure In-Memory-Database used for automated and unit tests.
        /// </param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// A database set representing companies.
        /// </summary>
        public DbSet<Company> Companies { get; set; }

        /// <summary>
        /// A database set representing employees.
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <inheritdoc/>
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