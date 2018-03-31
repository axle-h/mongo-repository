using System;
using Humanizer;
using MongoDB.Extensions.Repository.Configuration;

namespace MongoDB.Extensions.Repository.Extensions
{
    public static class MongoConfigurationExtensions
    {
        /// <summary>
        /// Gets the name of the mongo collection configured for the specified type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static string GetCollectionName<TEntity>(this MongoConfiguration configuration)
        {
            var name = typeof(TEntity).Name;
            if (configuration.PluralizeCollectionNames)
            {
                name = name.Pluralize();
            }

            switch (configuration.CollectionNamingConvention)
            {
                case NamingConvention.LowerCase:
                    return name.ToLower();

                case NamingConvention.UpperCase:
                    return name.ToUpper();

                case NamingConvention.Pascal:
                    return name.Pascalize();

                case NamingConvention.Camel:
                    return name.Camelize();

                case NamingConvention.Snake:
                    return name.Underscore();

                default:
                    throw new ArgumentOutOfRangeException(nameof(configuration.CollectionNamingConvention),
                                                          configuration.CollectionNamingConvention,
                                                          "Unknown collection naming convention");
            }
        }
    }
}
