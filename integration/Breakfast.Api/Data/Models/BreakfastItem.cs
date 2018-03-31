using MongoDB.Extensions.Repository.Models;

namespace Breakfast.Api.Data.Models
{
    public class BreakfastItem : MongoEntity
    {
        /// <summary>
        /// Gets or sets the name of this breakfast item.
        /// </summary>
        public string Name { get; set; }
    }
}
