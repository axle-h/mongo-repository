using Breakfast.Api.Configuration;
using Breakfast.Api.Data.Models;
using Microsoft.Extensions.Options;
using MongoDB.Extensions.Repository.Configuration;
using MongoDB.Extensions.Repository.Indexes;

namespace Breakfast.Api.Data.Indexes
{
    /// <summary>
    /// Mongo indexes for <see cref="BreakfastOrder"/>.
    /// </summary>
    /// <seealso cref="BreakfastOrder" />
    public class BreakfastOrderIndexProfile : ExpiringMongoIndexProfile<BreakfastOrder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastOrderIndexProfile"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public BreakfastOrderIndexProfile(IOptions<BreakfastApiOptions> options) : base(options.Value.BreakfastOrderExpireTime)
        {
        }
    }
}
