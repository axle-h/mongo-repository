using MongoDB.Extensions.Repository.Models;

namespace Breakfast.Api.Data.Models
{
    public class BreakfastReview : SoftDeletableMongoEntity
    {
        /// <summary>
        /// Gets or sets the username of the reviewer.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the rating from 1 to 10.
        /// </summary>
        public int Rating { get; set; }
    }
}
