using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Configuration;
using MongoDB.Extensions.Repository.Interfaces;

namespace MongoDB.Extensions.Repository
{
    /// <summary>
    /// Context used to maintain a single MongoDB connection.
    /// </summary>
    /// <seealso cref="IMongoContext" />
    /// <seealso cref="System.IDisposable" />
    public class MongoContext : IMongoContext, IDisposable
    {
        private readonly SemaphoreSlim _semaphore;
        private readonly MongoUrl _url;
        private readonly MongoConfiguration _options;
        private readonly IEnumerable<IMongoIndexProfile> _indexBuilders;
        private IMongoDatabase _database;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoContext" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="indexBuilders">The index builders.</param>
        public MongoContext(IOptions<MongoConfiguration> options, IEnumerable<IMongoIndexProfile> indexBuilders)
        {
            _indexBuilders = indexBuilders;
            _options = options.Value;

            var connectionString = options.Value.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Must provide a mongo connection string");
            }

            _url = new MongoUrl(connectionString);
            if (string.IsNullOrEmpty(_url.DatabaseName))
            {
                throw new ArgumentNullException(nameof(connectionString), "Must provide a database name with the mongo connection string");
            }

            _semaphore = new SemaphoreSlim(1, 1);
        }

        /// <summary>
        /// Gets the MongoDB collection for the specified type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<IMongoCollection<TEntity>> GetCollectionAsync<TEntity>(CancellationToken cancellationToken = default)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MongoContext));
            }

            try
            {
                await _semaphore.WaitAsync(cancellationToken);
                var collectionName = typeof(TEntity).Name.Pluralize().Underscore();
                if (_database == null)
                {
                    _database = new MongoClient(MongoClientSettings.FromUrl(_url)).GetDatabase(_url.DatabaseName);
                    var tasks = _indexBuilders.Select(b => b.CreateIndexesAsync(_database, _options, cancellationToken));
                    await Task.WhenAll(tasks);
                }
                return _database.GetCollection<TEntity>(collectionName);
            }
            finally
            {
                _semaphore.Release();
            }
        }


        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            lock (_semaphore)
            {
                if (_disposed)
                {
                    return;
                }
                _disposed = true;
            }

            _semaphore.Dispose();
            _database?.Client.Cluster.Dispose();
        }
    }
}
