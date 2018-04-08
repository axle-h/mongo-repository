using Breakfast.Api.Data.Models;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Seed;

namespace Breakfast.Api.Data.Seeds
{
    /// <summary>
    /// A seed profile for <see cref="BreakfastReview"/>.
    /// </summary>
    /// <seealso cref="MongoDB.Extensions.Repository.Seed.MongoSeedProfile{Breakfast.Api.Data.Models.BreakfastReview}" />
    public class BreakfastReviewSeedProfile : MongoSeedProfile<BreakfastReview>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastReviewSeedProfile"/> class.
        /// </summary>
        public BreakfastReviewSeedProfile()
        {
            Add(new BreakfastReview
                {
                    Username = "seed_user",
                    Rating = 5
                });
        }

        protected override FilterDefinition<BreakfastReview> GetKeyFilter(BreakfastReview entity) => Filter.Eq(x => x.Username, entity.Username);
    }
}
