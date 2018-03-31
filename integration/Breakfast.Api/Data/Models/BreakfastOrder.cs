using System.Collections.Generic;
using MongoDB.Extensions.Repository.Models;

namespace Breakfast.Api.Data.Models
{
    public class BreakfastOrder : ExpiringMongoEntity
    {
        /// <summary>
        /// Gets or sets the breakfast items.
        /// </summary>
        public ICollection<string> BreakfastItems { get; set; }
    }
}
