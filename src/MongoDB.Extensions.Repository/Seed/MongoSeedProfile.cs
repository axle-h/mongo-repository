using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Configuration;
using MongoDB.Extensions.Repository.Extensions;
using MongoDB.Extensions.Repository.Interfaces;
using MongoDB.Extensions.Repository.Models;

namespace MongoDB.Extensions.Repository.Seed
{
    /// <summary>
    /// A profile for seeding mongo data.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class MongoSeedProfile<TEntity> : IMongoSeedProfile
        where TEntity : MongoEntity
    {
        private readonly List<TEntity> _seeds;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoSeedProfile{TEntity}"/> class.
        /// </summary>
        protected MongoSeedProfile()
        {
            _seeds = new List<TEntity>();
        }

        /// <summary>
        /// Adds the specified seed entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected void Add(TEntity entity) => _seeds.Add(entity);

        /// <summary>
        /// Adds the specified seed entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        protected void AddRange(params TEntity[] entities) => _seeds.AddRange(entities);

        /// <summary>
        /// Gets a filter that will be used to determine whether the specified seed entity already exists.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        protected abstract FilterDefinition<TEntity> GetKeyFilter(TEntity entity);

        /// <summary>
        /// Creates the seeds asynchronous.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task CreateSeedsAsync(IMongoDatabase database, MongoConfiguration configuration, CancellationToken cancellationToken = default)
        {
            var collectionName = configuration.GetCollectionName<TEntity>();
            var collection = database.GetCollection<TEntity>(collectionName);

            var tasks = _seeds.Select(async seed =>
                                        {
                                            var filter = GetKeyFilter(seed);
                                            var cursor = await collection.FindAsync(filter, cancellationToken: cancellationToken);
                                            if (await cursor.AnyAsync(cancellationToken))
                                            {
                                                return;
                                            }

                                            await collection.InsertOneAsync(seed, cancellationToken: cancellationToken);
                                        });
            await Task.WhenAll(tasks);
        }

        protected static FilterDefinitionBuilder<TEntity> Filter { get; } = Builders<TEntity>.Filter;
    }
}
