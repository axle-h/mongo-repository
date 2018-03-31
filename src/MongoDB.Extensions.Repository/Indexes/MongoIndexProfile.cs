using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Configuration;
using MongoDB.Extensions.Repository.Interfaces;
using MongoDB.Extensions.Repository.Models;
using MongoDB.Extensions.Repository.Extensions;

namespace MongoDB.Extensions.Repository.Indexes
{
    /// <summary>
    /// Defines indexes for mongo collections of the specified entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class MongoIndexProfile<TEntity> : IMongoIndexProfile
            where TEntity : MongoEntity
    {
        private readonly ICollection<CreateIndexModel<TEntity>> _indexes;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoIndexProfile{TEntity}"/> class.
        /// </summary>
        protected MongoIndexProfile()
        {
            _indexes = new List<CreateIndexModel<TEntity>>();
        }

        /// <summary>
        /// Creates the configured indexes using the specified mongo database and configuration.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task CreateIndexesAsync(IMongoDatabase database, MongoConfiguration configuration, CancellationToken cancellationToken = default)
        {
            var collectionName = configuration.GetCollectionName<TEntity>();
            var collection = database.GetCollection<TEntity>(collectionName);

            var tasks = _indexes.Select(async index =>
                                        {
                                            try
                                            {
                                                await collection.Indexes.CreateOneAsync(index.Keys, index.Options, cancellationToken);
                                            }
                                            catch (MongoCommandException)
                                            {
                                                // Drop and try again.
                                                await collection.Indexes.DropOneAsync(index.Options.Name, cancellationToken);
                                                await collection.Indexes.CreateOneAsync(index.Keys, index.Options, cancellationToken);
                                            }
                                        });
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Adds the specified index keys.
        /// </summary>
        /// <param name="name">The name of the index.</param>
        /// <param name="keys">The keys.</param>
        /// <param name="optionsConfigurator">The options configurator.</param>
        protected void Add(string name, IndexKeysDefinition<TEntity> keys, Action<CreateIndexOptions> optionsConfigurator = null)
        {
            if (_indexes.Any(x => x.Options.Name == name))
            {
                throw new ArgumentException($"An index with the name {name} has already been added", nameof(name));
            }

            var options = new CreateIndexOptions
            {
                Name = name
            };
            optionsConfigurator?.Invoke(options);
            _indexes.Add(new CreateIndexModel<TEntity>(keys, options));
        }

        protected IndexKeysDefinitionBuilder<TEntity> IndexKeys { get; } = Builders<TEntity>.IndexKeys;
    }
}
