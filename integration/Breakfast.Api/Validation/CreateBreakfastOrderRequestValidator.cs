using System.Linq;
using Breakfast.Api.Models.BreakfastOrder;
using FluentValidation;

namespace Breakfast.Api.Validation
{
    /// <summary>
    /// Validator for <see cref="CreateBreakfastOrderRequest"/>.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Breakfast.Api.Models.BreakfastOrder.CreateBreakfastOrderRequest}" />
    public class CreateBreakfastOrderRequestValidator : AbstractValidator<CreateBreakfastOrderRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBreakfastOrderRequestValidator" /> class.
        /// </summary>
        public CreateBreakfastOrderRequestValidator()
        {
            RuleFor(x => x.BreakfastItems).NotEmpty()
                                          .Must(x => x.All(s => !string.IsNullOrEmpty(s)))
                                          .WithMessage("All '{PropertyName}' should not be empty.")
                                          .Must(x => x.Distinct().Count() == x.Count)
                                          .WithMessage("'{PropertyName}' should not contain duplicates.");
        }
    }
}
