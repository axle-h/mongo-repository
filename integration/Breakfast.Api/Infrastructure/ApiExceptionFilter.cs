using Breakfast.Api.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Breakfast.Api.Infrastructure
{
    /// <summary>
    /// Catches and handles some custom exceptions.
    /// <see cref="EntityNotFoundException"/>: returns a 404.
    /// <see cref="BadRequestException"/>: returns a 400.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute" />
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// Called when an exception is thrown by the executing action.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void OnException(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case EntityNotFoundException e:
                    context.ExceptionHandled = true;
                    context.ModelState.AddModelError(string.Empty, e.Message);
                    context.Result = new NotFoundObjectResult(new SerializableError(context.ModelState));
                    break;

                case BadRequestException e:
                    context.ExceptionHandled = true;
                    foreach (var error in e.Validation.Errors)
                    {
                        context.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    context.Result = new BadRequestObjectResult(context.ModelState);
                    break;
            }
        }
    }
}
