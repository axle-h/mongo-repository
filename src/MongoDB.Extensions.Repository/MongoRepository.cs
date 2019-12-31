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
        private readonly Lazy<Task<IMongoCollection<TEntity>>> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public MongoRepository(IMongoContext context)
        {
            _collection = new Lazy<Task<IMongoCollection<TEntity>>>(
                () => context.GetCollectionAsync<TEntity>(CancellationToken.None),
                LazyThreadSafetyMode.ExecutionAndPublication);
        }

        /// <summary>
        /// Gets the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(string id, CancellationToken cancellationToken = default) =>
            await FindOneAsync(Filter.IdEq(id), null, cancellationToken);

        /// <summary>
        /// Gets all entities in this repository.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await FindAsync(Filter.Empty, null, cancellationToken);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var collection = await _collection.Value;
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
            var collection = await _collection.Value;
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
            var collection = await _collection.Value;
            var result = await collection.FindOneAndDeleteAsync(Filter.IdEq(id), cancellationToken: cancellationToken);
            return result?.Id == id;
        }

        /// <summary>
        /// Replaces the specified entity with the same identifier.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The replaced document.</returns>
        public virtual async Task<TEntity> ReplaceAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var collection = await _collection.Value;
            return await collection.FindOneAndReplaceAsync(
                Filter.IdEq(entity.Id), entity,
                new FindOneAndReplaceOptions<TEntity> { ReturnDocument = ReturnDocument.After },
                cancellationToken);
        }

        /// <summary>
        /// Updates the entity with the specified key according to the specified update definition.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updateDefinition">The update definition.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected async Task<TEntity> UpdateAsync(string id, UpdateDefinition<TEntity> updateDefinition, FindOneAndUpdateOptions<TEntity> options = null, CancellationToken cancellationToken = default)
        {
            var collection = await _collection.Value;
            return await collection.FindOneAndUpdateAsync(Filter.IdEq(id), updateDefinition, options, cancellationToken);
        }

        /// <summary>
        /// Finds the entity according to the specified filter definition.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected async Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filter, FindOptions<TEntity> options = null, CancellationToken cancellationToken = default)
        {
            var collection = await _collection.Value;
            var cursor = await collection.FindAsync(filter, options, cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Finds all entities according to the specified filter definition.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        protected async Task<ICollection<TEntity>> FindAsync(FilterDefinition<TEntity> filter, FindOptions<TEntity> options = null, CancellationToken cancellationToken = default)
        {
            var collection = await _collection.Value;
            var cursor = await collection.FindAsync(filter, options, cancellationToken);
            return await cursor.ToListAsync(cancellationToken);
        }
        
        protected static FilterDefinitionBuilder<TEntity> Filter => Builders<TEntity>.Filter;

        protected static SortDefinitionBuilder<TEntity> Sort => Builders<TEntity>.Sort;

        protected static UpdateDefinitionBuilder<TEntity> Update => Builders<TEntity>.Update;

        protected static ProjectionDefinitionBuilder<TEntity> Projection => Builders<TEntity>.Projection;
    }
}
