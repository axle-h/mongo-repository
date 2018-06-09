using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Breakfast.Api.Extensions
{
    internal static class ExpressionExtensions
    {
        public static PropertyInfo GetPropertyInfo<TEntity, TProperty>(this Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            PropertyInfo FromMemberExpression(MemberExpression me)
            {
                var property = me.Member as PropertyInfo ?? throw new ArgumentException("Invalid property expression", nameof(propertyExpression));
                return property;
            }

            switch (propertyExpression.Body)
            {
                case MemberExpression me:
                    return FromMemberExpression(me);

                case UnaryExpression ue:
                    var operand = ue.Operand as MemberExpression ?? throw new ArgumentException("Invalid property expression", nameof(propertyExpression));
                    return FromMemberExpression(operand);

                default:
                    throw new ArgumentException("Invalid property expression", nameof(propertyExpression));
            }
        }
    }
}
