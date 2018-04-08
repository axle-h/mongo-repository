namespace Breakfast.Api.Models.BreakfastReview
{
    /// <summary>
    /// A request to update a breakfast review.
    /// </summary>
    public class UpdateBreakfastReviewRatingRequest
    {
        /// <summary>
        /// Gets or sets the rating from 1 to 10.
        /// </summary>
        public int Rating { get; set; }
    }
}
