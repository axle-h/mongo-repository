using Breakfast.Api.Models;
using Breakfast.Api.Models.BreakfastItem;
using FluentValidation;

namespace Breakfast.Api.Validation
{
    /// <summary>
    /// Validator for <see cref="CreateBreakfastItemRequest"/>.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Breakfast.Api.Models.CreateBreakfastItemRequest}" />
    public class CreateBreakfastItemRequestValidator : AbstractValidator<CreateBreakfastItemRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateBreakfastItemRequestValidator"/> class.
        /// </summary>
        public CreateBreakfastItemRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
