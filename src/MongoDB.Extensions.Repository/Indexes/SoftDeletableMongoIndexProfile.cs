using MongoDB.Driver;
using MongoDB.Extensions.Repository.Models;

namespace MongoDB.Extensions.Repository.Indexes
{
    /// <summary>
    /// Defines indexes for mongo collections of the specified soft deletable entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="MongoIndexProfile{TEntity}" />
    public abstract class SoftDeletableMongoIndexProfile<TEntity> : MongoIndexProfile<TEntity>
        where TEntity : SoftDeletableMongoEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoftDeletableMongoIndexProfile{TEntity}"/> class.
        /// </summary>
        protected SoftDeletableMongoIndexProfile()
        {
            Add("date_deleted", Builders<TEntity>.IndexKeys.Ascending(x => x.DateDeleted));
        }
    }
}