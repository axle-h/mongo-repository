using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Extensions.Repository.Interfaces;

namespace MongoDB.Extensions.Repository.Configuration
{
    /// <summary>
    /// A configuration builder for this package.
    /// </summary>
    public class MongoConfigurationBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MongoConfigurationBuilder"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        public MongoConfigurationBuilder(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// Gets the service collection.
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Searches the specified assembly for type and registers them.
        /// * Registers all public implementations of <see cref="IMongoRepository{TEntity}"/> as each of their non-generic interfaces.
        /// * Adds indexes defined in public implementations of <see cref="IMongoIndexProfile"/>.
        /// 
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public MongoConfigurationBuilder FromAssembly(Assembly assembly)
        {
            // Repositories.
            var repositoryTypes = assembly.ExportedTypes
                                          .Where(t => !t.IsAbstract &&
                                                      !t.IsInterface &&
                                                      t.GetInterfaces().Any(i => i.IsGenericType &&
                                                                                 i.GetGenericTypeDefinition() == typeof(IMongoRepository<>)));
            foreach (var repositoryType in repositoryTypes)
            {
                foreach (var i in repositoryType.GetInterfaces().Where(i => !i.IsGenericType))
                {
                    Services.AddTransient(i, repositoryType);
                }

            }

            // Indexes.
            var indexes = assembly.ExportedTypes.Where(t => !t.IsAbstract && !t.IsInterface && typeof(IMongoIndexProfile).IsAssignableFrom(t));
            foreach (var index in indexes)
            {
                Services.AddSingleton(typeof(IMongoIndexProfile), index);
            }

            // Seeds.
            var seeds = assembly.ExportedTypes.Where(t => !t.IsAbstract && !t.IsInterface && typeof(IMongoSeedProfile).IsAssignableFrom(t));
            foreach (var seed in seeds)
            {
                Services.AddSingleton(typeof(IMongoSeedProfile), seed);
            }

            return this;
        }

        /// <summary>
        /// Registers all public implementations of <see cref="IMongoRepository{TEntity}"/> as each of their non-generic interfaces.
        /// Adds indexes defined in public implementations of <see cref="IMongoIndexProfile"/>.
        /// </summary>
        /// <returns></returns>
        public MongoConfigurationBuilder FromAssemblyContaining<T>() => FromAssembly(typeof(T).Assembly);
    }
}
