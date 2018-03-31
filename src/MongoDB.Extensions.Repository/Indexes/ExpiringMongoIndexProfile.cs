using System;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Models;
using MongoDB.Extensions.Repository.Extensions;

namespace MongoDB.Extensions.Repository.Indexes
{
    /// <summary>
    /// Defines indexes for mongo collections of the specified expiring entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="MongoIndexProfile{TEntity}" />
    public abstract class ExpiringMongoIndexProfile<TEntity> : MongoIndexProfile<TEntity>
        where TEntity : ExpiringMongoEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpiringMongoIndexProfile{TEntity}"/> class.
        /// </summary>
        /// <param name="expireAfter">The expire after.</param>
        protected ExpiringMongoIndexProfile(TimeSpan expireAfter)
        {
            Add("date_created_expire", Builders<TEntity>.IndexKeys.Ascending(x => x.DateCreated), o => o.ExpireAfter(expireAfter));
        }
    }
}