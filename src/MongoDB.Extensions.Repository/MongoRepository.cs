using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Interfaces;
using MongoDB.Extensions.Repository.Models;
using MongoDB.Extensions.Repository.Extensions;

namespace MongoDB.Extensions.Repository
{
    /// <summary>
    /// A MongoDB based repository of <see cref="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class MongoRepository<TEntity> : IMongoRepository<TEntity>
        where TEntity : MongoEntity
    {
        private readonly IMongoContext _context;
        private readonly AsyncLocal<IMongoCollection<TEntity>> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MongoRepository(IMongoContext context)
        {
            _context = context;
            _collection = new AsyncLocal<IMongoCollection<TEntity>>();
        }

        /// <summary>
        /// Gets the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(string id, CancellationToken cancellationToken = default) =>
            await FindOneAsync(Filter.IdEq(id), cancellationToken);

        /// <summary>
        /// Gets all entities in this repository.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await FindAsync(Filter.Empty, cancellationToken);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var collection = await GetCollectionAsync(cancellationToken);
            await collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Adds the specified entities.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual async Task AddManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            var collection = await GetCollectionAsync(cancellationToken);
            await collection.InsertManyAsync(entities, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Deletes the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var collection = await GetCollectionAsync(cancellationToken);
            var result = await collection.FindOneAndDeleteAsync(Filter.IdEq(id), cancellationToken: cancellationToken);
            return result?.Id == id;
        }

        /// <summary>
        /// Replaces the specified entity with the same identifier.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual async Task<TEntity> ReplaceAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var collection = await GetCollectionAsync(cancellationToken);
            return await collection.FindOneAndReplaceAsync(Filter.IdEq(entity.Id), entity, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Updates the entity with the specified key according to the specified update definition.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updateDefinition">The update definition.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected async Task<TEntity> UpdateAsync(string id, UpdateDefinition<TEntity> updateDefinition, CancellationToken cancellationToken)
        {
            var collection = await GetCollectionAsync(cancellationToken);
            return await collection.FindOneAndUpdateAsync(Filter.IdEq(id), updateDefinition, cancellationToken: cancellationToken);
        }

        protected async Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken, FindOptions<TEntity> options = null)
        {
            var collection = await GetCollectionAsync(cancellationToken);
            var cursor = await collection.FindAsync(filter, options, cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

        protected async Task<ICollection<TEntity>> FindAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken, FindOptions<TEntity> options = null)
        {
            var collection = await GetCollectionAsync(cancellationToken);
            var cursor = await collection.FindAsync(filter, options, cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }

        protected async Task<IMongoCollection<TEntity>> GetCollectionAsync(CancellationToken cancellationToken)
        {
            if (_collection.Value != null)
            {
                return _collection.Value;
            }

            _collection.Value = await _context.GetCollectionAsync<TEntity>(cancellationToken);
            return _collection.Value;
        }

        protected static FilterDefinitionBuilder<TEntity> Filter { get; } = Builders<TEntity>.Filter;
    }
}
