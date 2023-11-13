using CompanyEmployees.Persistence.Entities;
using CompanyEmployees.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployees.Persistence
{
    /// <summary>
    /// Class <c>ApplicationDbContext</c> represents the database context used to access the
    /// database from the repositories.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        private readonly AuditingInterceptor _auditingInterceptor;

        /// <summary>
        /// This constructor initializes an instance of the <c>ApplicationDbContext</c> class with
        /// the appropriate context options passed to the base class.
        /// </summary>
        /// <param name="options">
        /// <c>options</c> are the context options passed to the base class. this is needed to
        /// properly configure the connection string in case of Development/Production environment
        /// or to configure In-Memory-Database used for automated and unit tests.
        /// </param>
        /// <param name="auditingInterceptor">The auditing interceptor</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AuditingInterceptor auditingInterceptor) : base(options)
        {
            _auditingInterceptor = auditingInterceptor;
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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.AddInterceptors(_auditingInterceptor);
        }

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