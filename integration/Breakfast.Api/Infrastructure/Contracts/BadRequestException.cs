using System;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation.Results;
using Humanizer;

namespace Breakfast.Api.Infrastructure.Contracts
{
    /// <summary>
    /// An exception indicating a bad request with validation failures for messaging.
    /// This is handled by <see cref="ApiExceptionFilter"/>, returning a 400 bad request.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException" /> class.
        /// </summary>
        /// <param name="inner">The inner.</param>
        /// <param name="validationFailures">The validation failures.</param>
        /// <exception cref="ArgumentException">The validation result is valid - validationFailures</exception>
        public BadRequestException(Exception inner, params ValidationFailure[] validationFailures)
            : base(string.Join(", ", validationFailures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}")), inner)
        {
            if (!validationFailures.Any())
            {
                throw new ArgumentException("The validation result is valid", nameof(validationFailures));
            }

            Validation = new ValidationResult(validationFailures);
        }

        /// <summary>
        /// Gets the validation.
        /// </summary>
        public ValidationResult Validation { get; }

        /// <summary>
        /// Creates a bad request exception informing of a duplicate <see cref="TEntity" /> of the specified value.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="inner">The inner.</param>
        /// <returns></returns>
        public static BadRequestException Duplicate<TEntity>(Expression<Func<TEntity, object>> property, object value, Exception inner)
        {
            var member = property.Body as MemberExpression ?? throw new ArgumentException("not a member expression", nameof(property));
            var name = member.Member.Name;
            return new BadRequestException(inner, new ValidationFailure(name, $"A {typeof(TEntity).Name.Humanize()} already exists with {name.Humanize()} {value}"));
        }
    }
}
