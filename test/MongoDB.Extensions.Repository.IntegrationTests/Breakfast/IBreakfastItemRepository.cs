using System.Threading;
using System.Threading.Tasks;
using MongoDB.Extensions.Repository.Interfaces;

namespace MongoDB.Extensions.Repository.IntegrationTests.Breakfast
{
    /// <summary>
    /// A MongoDB based repository of <see cref="BreakfastItem" />.
    /// </summary>
    public interface IBreakfastItemRepository : IMongoRepository<BreakfastItem>
    {
        /// <summary>
        /// Gets the breakfast item with the specified name. Or <c>null</c> if none exist.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<BreakfastItem> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}