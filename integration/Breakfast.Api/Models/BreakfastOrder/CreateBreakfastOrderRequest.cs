using System.Collections.Generic;

namespace Breakfast.Api.Models.BreakfastOrder
{
    /// <summary>
    /// A request to create a new breakfast order.
    /// </summary>
    public class CreateBreakfastOrderRequest
    {
        /// <summary>
        /// Gets or sets the breakfast items.
        /// </summary>
        public ICollection<string> BreakfastItems { get; set; }
    }
}
