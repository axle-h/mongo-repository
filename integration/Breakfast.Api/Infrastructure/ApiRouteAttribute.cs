using Microsoft.AspNetCore.Mvc;

namespace Breakfast.Api.Infrastructure
{
    /// <summary>
    /// Defines a route to an API controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.RouteAttribute" />
    public class ApiRouteAttribute : RouteAttribute
    {
        private const string ApiRouteBase = "api/[controller]";

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiRouteAttribute"/> class.
        /// </summary>
        /// <param name="template">The route template. May not be null.</param>
        public ApiRouteAttribute(string template = null) : base(string.IsNullOrEmpty(template)
                                                                    ? ApiRouteBase
                                                                    : $"{ApiRouteBase}/{template.TrimStart('/')}")
        {
        }
    }
}
