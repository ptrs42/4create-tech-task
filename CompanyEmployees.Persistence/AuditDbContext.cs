using CompanyEmployees.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployees.Persistence
{
    /// <summary>
    /// Class <c>AuditDbContext</c> represents the database context used for audit purposes
    /// </summary>
    public class AuditDbContext : DbContext
    {
        /// <summary>
        /// This constructor initializes an instance of the <c>AuditDbContext</c> class with the
        /// appropriate context options passed to the base class.
        /// </summary>
        /// <param name="options">
        /// <c>options</c> are the context options passed to the base class. this is needed to
        /// properly configure the connection string in case of Development/Production environment
        /// or to configure In-Memory-Database used for automated and unit tests.
        /// </param>
        public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// A database set representing audit entries.
        /// </summary>
        public DbSet<SystemLog> SystemLogs { get; set; }
    }
}