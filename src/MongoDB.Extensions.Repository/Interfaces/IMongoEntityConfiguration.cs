using MongoDB.Extensions.Repository.Configuration;
using MongoDB.Extensions.Repository.Models;

namespace MongoDB.Extensions.Repository.Interfaces
{
    /// <summary>
    /// Mongo entity configuration.
    /// </summary>
    public interface IMongoEntityConfiguration<TEntity>
        where TEntity : MongoEntity
    {
        void Configure(MongoEntityBuilder<TEntity> context);
    }
}