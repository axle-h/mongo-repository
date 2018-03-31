using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Configuration;

namespace MongoDB.Extensions.Repository.Interfaces
{
    public interface IMongoIndexProfile
    {
        /// <summary>
        /// Creates the configured indexes using the specified mongo database and configuration.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task CreateIndexesAsync(IMongoDatabase database, MongoConfiguration configuration, CancellationToken cancellationToken = default);
    }
}