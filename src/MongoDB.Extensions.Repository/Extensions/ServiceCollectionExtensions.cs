using System;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Extensions.Repository.Configuration;
using MongoDB.Extensions.Repository.Interfaces;

namespace MongoDB.Extensions.Repository.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/> to add easy MongoDB wiring.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the MongoDB context with the specified service collection.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configurator">The configurator.</param>
        /// <returns></returns>
        /// <remarks>
        /// This currently requires wiring up memory caching and logging.
        /// </remarks>
        public static MongoConfigurationBuilder AddMongoRepositories(this IServiceCollection services, Action<MongoConfiguration> configurator)
        {
            services.Configure(configurator);
            services.AddSingleton<IMongoContext, MongoContext>();
            services.AddTransient(typeof(IMongoRepository<>), typeof(MongoRepository<>));
            services.AddTransient(typeof(ISoftDeletableMongoRepository<>), typeof(SoftDeletableMongoRepository<>));
            return new MongoConfigurationBuilder(services);
        }

        /// <summary>
        /// Registers the MongoDB context with the specified service collection.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        /// <remarks>
        /// This currently requires wiring up memory caching and logging.
        /// </remarks>
        public static MongoConfigurationBuilder AddMongoRepositories(this IServiceCollection services, string connectionString) =>
            services.AddMongoRepositories(c => c.ConnectionString = connectionString);
    }
}
