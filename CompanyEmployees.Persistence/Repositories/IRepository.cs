using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace CompanyEmployees.Persistence.Repositories
{
    /// <summary>
    /// Interface <see cref="IRepository{TEntity}"/> can be used to query and save instances of
    /// <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// This method adds asynchroniously an instance of <typeparamref name="TEntity"/> entity to
        /// the database.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <returns>The instance of the persisted entity.</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// This method checks asynchroniously if all entities in a set comply with the predicate.
        /// </summary>
        /// <param name="predicate">The predicate to check against.</param>
        /// <returns>True if all entities in a set comply with the predicate; otherwise, false.</returns>
        Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// This method checks asynchroniously if any of the entities in a set comply with the predicate.
        /// </summary>
        /// <param name="predicate">The predicate to check against.</param>
        /// <returns>
        /// True if any of the entities in a set comply with the predicate; otherwise, false.
        /// </returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Asynchronously starts a new transaction. The transaction automatically rolls back if all
        /// queries were not successful when the transaction instance goes out of scope
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous transaction initialization. The task result
        /// contains a <see cref="IDbContextTransaction"/> that represents the started transaction.
        /// </returns>
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        /// Filters the set of enitites based on the predicate.
        /// </summary>
        /// <param name="predicate">The predicate to filter on.</param>
        /// <returns>A collection of entities from the input set that satisfy the predicate.</returns>
        IEnumerable<TEntity> Where(Func<TEntity, bool> predicate);

        /// <summary>
        /// Filters the set of enitites based on the predicate and includes related entities.
        /// </summary>
        /// <param name="predicate">The predicate to filter on.</param>
        /// <param name="include">Specifies the related entities to include.</param>
        /// <returns>
        /// A collection of entities from the input set that satisfy the predicate with related data included.
        /// </returns>
        IEnumerable<TEntity> WhereInclude<TProperty>(Func<TEntity, bool> predicate, Expression<Func<TEntity, TProperty>> include);
    }
}