using System.Threading;
using System.Threading.Tasks;
using Breakfast.Api.Data.Models;
using MongoDB.Extensions.Repository.Interfaces;

namespace Breakfast.Api.Data.Interfaces
{
    /// <summary>
    /// A repository of <see cref="BreakfastReview"/>.
    /// </summary>
    /// <seealso cref="MongoDB.Extensions.Repository.Interfaces.IMongoRepository{BreakfastReview}" />
    public interface IBreakfastReviewRepository : ISoftDeletableMongoRepository<BreakfastReview>
    {
        /// <summary>
        /// Updates the rating of the breakfast review with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="rating">The rating.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns></returns>
        Task<BreakfastReview> UpdateRatingAsync(string id, int rating, CancellationToken token = default);
    }
}