using MongoDB.Driver;
using MongoDB.Extensions.Repository.Configuration;
using MongoDB.Extensions.Repository.Extensions;
using MongoDB.Extensions.Repository.Interfaces;

namespace MongoDB.Extensions.Repository.IntegrationTests.Breakfast
{
    /// <summary>
    /// Mongo entity configuration for <see cref="BreakfastItem"/>.
    /// </summary>
    /// <seealso cref="BreakfastItem" />
    public class BreakfastItemEntityConfiguration : IMongoEntityConfiguration<BreakfastItem>
    {
        public static readonly BreakfastItem Seed = new BreakfastItem
        {
            Id = "5e0b29f7a2077ef4078c049b",
            Name = "Bacon"
        };

        /// <summary>
        /// Configures the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Configure(MongoEntityBuilder<BreakfastItem> context)
        {
            context.Indexes.Add("name_unique",
                Builders<BreakfastItem>.IndexKeys.Ascending(x => x.Name),
                o => o.Unique().WithCaseInsensitiveCollation());

            context.Seed.Add(Seed);
        }
    }
}
