using System.Threading;
using System.Threading.Tasks;
using Breakfast.Api.Data.Interfaces;
using Breakfast.Api.Data.Models;
using MongoDB.Driver;
using MongoDB.Extensions.Repository;
using MongoDB.Extensions.Repository.Interfaces;

namespace Breakfast.Api.Data
{
    /// <summary>
    /// A mongo repository of <see cref="BreakfastReview"/>.
    /// </summary>
    /// <seealso cref="MongoDB.Extensions.Repository.MongoRepository{BreakfastReview}" />
    public class BreakfastReviewRepository : SoftDeletableMongoRepository<BreakfastReview>, IBreakfastReviewRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastReviewRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BreakfastReviewRepository(IMongoContext context) : base(context)
        {
        }

        /// <summary>
        /// Updates the rating of the breakfast review with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="rating">The rating.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        public async Task<BreakfastReview> UpdateRatingAsync(string id, int rating, CancellationToken token = default)
        {
            return await UpdateAsync(id, Update.Set(x => x.Rating, rating), token, new FindOneAndUpdateOptions<BreakfastReview> {ReturnDocument = ReturnDocument.After});
        }
    }
}
