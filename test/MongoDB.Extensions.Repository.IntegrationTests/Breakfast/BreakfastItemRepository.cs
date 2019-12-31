using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Extensions;
using MongoDB.Extensions.Repository.Interfaces;

namespace MongoDB.Extensions.Repository.IntegrationTests.Breakfast
{
    /// <summary>
    /// A repository of <see cref="BreakfastItem"/>.
    /// </summary>
    /// <seealso cref="MongoDB.Extensions.Repository.MongoRepository{Breakfast.Api.Domain.BreakfastItem}" />
    public class BreakfastItemRepository : MongoRepository<BreakfastItem>, IBreakfastItemRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastItemRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BreakfastItemRepository(IMongoContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets the breakfast item with the specified name. Or <c>null</c> if none exist.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<BreakfastItem> GetByNameAsync(string name, CancellationToken cancellationToken = default) =>
            await FindOneAsync(Filter.Eq(x => x.Name, name), new FindOptions<BreakfastItem>().WithCaseInsensitiveCollation(), cancellationToken);
    }
}
