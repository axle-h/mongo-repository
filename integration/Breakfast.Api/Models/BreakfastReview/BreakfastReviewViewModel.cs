namespace Breakfast.Api.Models.BreakfastReview
{
    public class BreakfastReviewViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the reviewer.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the rating from 1 to 10.
        /// </summary>
        public int Rating { get; set; }
    }
}
