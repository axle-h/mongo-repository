using Breakfast.Api.Data.Models;
using MongoDB.Extensions.Repository.Indexes;

namespace Breakfast.Api.Data.Indexes
{
    /// <summary>
    /// Mongo indexes for <see cref="BreakfastReview"/>.
    /// </summary>
    /// <seealso cref="BreakfastReview" />
    public class BreakfastReviewIndexProfile : SoftDeletableMongoIndexProfile<BreakfastReview>
    {
    }
}
