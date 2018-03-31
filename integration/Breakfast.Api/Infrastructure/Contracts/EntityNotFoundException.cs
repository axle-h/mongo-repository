using System;
using System.Linq.Expressions;
using System.Reflection;
using Humanizer;

namespace Breakfast.Api.Infrastructure.Contracts
{
    /// <summary>
    /// An exception indicating that a requested entity does not exist.
    /// This is handled by <see cref="ApiExceptionFilter"/>, returning a 404 not found.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityNotFoundException"/> class.
        /// </summary>
        /// <param name="type">The type of entity that does not exist.</param>
        /// <param name="keyName">Name of the key that was used to access the entity.</param>
        /// <param name="key">The key value.</param>
        public EntityNotFoundException(MemberInfo type, string keyName, object key)
            : base($"Cannot find a {type.Name.Humanize()} with {keyName.Humanize()} {key}")
        {
            Type = type;
            KeyName = keyName;
            Key = key;
        }

        /// <summary>
        /// Gets the type of entity that does not exist.
        /// </summary>
        public MemberInfo Type { get; }

        /// <summary>
        /// Gets the name of the key that was used to access the entity.
        /// </summary>
        public string KeyName { get; }

        /// <summary>
        /// Gets the key value.
        /// </summary>
        public object Key { get; }

        /// <summary>
        /// Creates a new entity not found exception for the specified type, property and key value.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="property">The property.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static EntityNotFoundException Create<TEntity>(Expression<Func<TEntity, object>> property, object key)
        {
            var member = property.Body as MemberExpression ?? throw new ArgumentException("not a member expression", nameof(property));
            return new EntityNotFoundException(typeof(TEntity), member.Member.Name, key);
        }
    }
}
