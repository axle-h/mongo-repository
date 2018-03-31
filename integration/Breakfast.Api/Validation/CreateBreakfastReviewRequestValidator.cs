using Breakfast.Api.Models.BreakfastReview;
using FluentValidation;

namespace Breakfast.Api.Validation
{
    /// <summary>
    /// Validation for <see cref="CreateBreakfastReviewRequest"/>.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Breakfast.Api.Models.BreakfastReview.CreateBreakfastReviewRequest}" />
    public class CreateBreakfastReviewRequestValidator : AbstractValidator<CreateBreakfastReviewRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBreakfastReviewRequestValidator" /> class.
        /// </summary>
        public CreateBreakfastReviewRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Rating).InclusiveBetween(1, 10);
        }
    }
}
