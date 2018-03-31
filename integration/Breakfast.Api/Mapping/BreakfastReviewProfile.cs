using AutoMapper;
using Breakfast.Api.Data.Models;
using Breakfast.Api.Models.BreakfastReview;

namespace Breakfast.Api.Mapping
{
    /// <summary>
    /// Mapping for <see cref="BreakfastReview"/>.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class BreakfastReviewProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastReviewProfile" /> class.
        /// </summary>
        public BreakfastReviewProfile()
        {
            CreateMap<BreakfastReview, BreakfastReviewViewModel>();
            CreateMap<CreateBreakfastReviewRequest, BreakfastReview>();
        }
    }
}
