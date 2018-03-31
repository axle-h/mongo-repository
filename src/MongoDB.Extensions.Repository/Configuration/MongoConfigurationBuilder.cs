using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Interfaces;
using MongoDB.Extensions.Repository.Extensions;
using MongoDB.Extensions.Repository.Models;

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
        /// Registers all public implementations of <see cref="IMongoRepository{TEntity}"/> in the specified assembly as each of their non-generic interfaces.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public MongoConfigurationBuilder FromAssembly(Assembly assembly)
        {
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
            return this;
        }

        /// <summary>
        /// Adds indexes defined in the specified index builder type.
        /// </summary>
        /// <typeparam name="TIndexBuilder">The type of the index builder.</typeparam>
        /// <returns></returns>
        public MongoConfigurationBuilder WithIndexes<TIndexBuilder>()
            where TIndexBuilder : class, IMongoIndexProfile
        {
            Services.AddSingleton<IMongoIndexProfile, TIndexBuilder>();
            return this;
        }

        /// <summary>
        /// Adds indexes defined in public implementations of <see cref="IMongoIndexProfile"/> in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public MongoConfigurationBuilder WithIndexesFromAssembly(Assembly assembly)
        {
            var profiles = assembly.ExportedTypes.Where(t => !t.IsAbstract && !t.IsInterface && typeof(IMongoIndexProfile).IsAssignableFrom(t));
            foreach (var profileType in profiles)
            {
                Services.AddSingleton(typeof(IMongoIndexProfile), profileType);
            }
            return this;
        }
    }
}
