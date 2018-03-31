using Breakfast.Api.Data.Models;
using MongoDB.Extensions.Repository.Extensions;
using MongoDB.Extensions.Repository.Indexes;

namespace Breakfast.Api.Data.Indexes
{
    /// <summary>
    /// Mongo indexes for <see cref="BreakfastItem"/>.
    /// </summary>
    /// <seealso cref="BreakfastItem" />
    public class BreakfastItemIndexProfile : MongoIndexProfile<BreakfastItem>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastItemIndexProfile"/> class.
        /// </summary>
        public BreakfastItemIndexProfile()
        {
            Add("name_unique", IndexKeys.Ascending(x => x.Name), o => o.Unique().WithCaseInsensitiveCollation());
        }
    }
}
