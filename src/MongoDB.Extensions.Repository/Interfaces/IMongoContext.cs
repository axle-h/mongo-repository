using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoDB.Extensions.Repository.Interfaces
{
    /// <summary>
    /// Context used to maintain a single MongoDB connection.
    /// </summary>
    public interface IMongoContext
    {
        /// <summary>
        /// Gets the MongoDB collection for the specified type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<IMongoCollection<TEntity>> GetCollectionAsync<TEntity>(CancellationToken cancellationToken = default);
    }
}