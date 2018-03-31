namespace Breakfast.Api.Models.BreakfastItem
{
    /// <summary>
    /// A request to create a new breakfast item.
    /// </summary>
    public class CreateBreakfastItemRequest
    {
        /// <summary>
        /// Gets or sets the name of this breakfast item.
        /// </summary>
        public string Name { get; set; }
    }
}
