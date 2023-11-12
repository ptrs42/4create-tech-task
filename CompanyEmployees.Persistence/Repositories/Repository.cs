using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CompanyEmployees.Persistence.Repositories
{
    /// <summary>
    /// Class <see cref="Repository{TEntity}"/> is a generic implementation of repository pattern
    /// for any of the entity types
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// This constructor initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The database context to use for making queries.</param>
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                var rv = await _context.AddAsync(entity);

                await _context.SaveChangesAsync();

                return rv.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await _context.Set<TEntity>().AllAsync(predicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await _context.Set<TEntity>().AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> Where(Func<TEntity, bool> predicate)
        {
            try
            {
                return _context.Set<TEntity>().Where(predicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> WhereInclude<TProperty>(Func<TEntity, bool> predicate, Expression<Func<TEntity, TProperty>> include)
        {
            try
            {
                return _context.Set<TEntity>().Include(include).Where(predicate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}