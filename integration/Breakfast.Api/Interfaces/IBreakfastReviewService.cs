using System.Collections.Generic;
using System.Threading.Tasks;
using Breakfast.Api.Models.BreakfastReview;

namespace Breakfast.Api.Interfaces
{
    public interface IBreakfastReviewService
    {
        /// <summary>
        /// Un-deletes the <see cref="BreakfastReviewViewModel"/> with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task UnDeleteAsync(string id);

        /// <summary>
        /// Creates a new <see cref="BreakfastReviewViewModel"/> according to the specified <see cref="CreateBreakfastReviewRequest"/>.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<BreakfastReviewViewModel> CreateAsync(CreateBreakfastReviewRequest request);

        /// <summary>
        /// Gets all <see cref="BreakfastReviewViewModel"/>.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<BreakfastReviewViewModel>> GetAllAsync();

        /// <summary>
        /// Gets the <see cref="BreakfastReviewViewModel"/> with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<BreakfastReviewViewModel> GetAsync(string id);

        /// <summary>
        /// Deletes the <see cref="BreakfastReviewViewModel"/> with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// Updates the breakfast review with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<BreakfastReviewViewModel> UpdateRatingAsync(string id, UpdateBreakfastReviewRatingRequest request);
    }
}