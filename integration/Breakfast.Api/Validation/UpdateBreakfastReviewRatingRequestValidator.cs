using Breakfast.Api.Models.BreakfastReview;
using FluentValidation;

namespace Breakfast.Api.Validation
{
    /// <summary>
    /// Validator for <see cref="UpdateBreakfastReviewRatingRequest"/>.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Breakfast.Api.Models.BreakfastReview.UpdateBreakfastReviewRequest}" />
    public class UpdateBreakfastReviewRatingRequestValidator : AbstractValidator<UpdateBreakfastReviewRatingRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateBreakfastReviewRatingRequestValidator" /> class.
        /// </summary>
        public UpdateBreakfastReviewRatingRequestValidator()
        {
            RuleFor(x => x.Rating).InclusiveBetween(1, 10);
        }
    }
}
