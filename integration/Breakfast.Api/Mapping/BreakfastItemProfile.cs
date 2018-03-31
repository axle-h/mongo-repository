using AutoMapper;
using Breakfast.Api.Data.Models;
using Breakfast.Api.Models.BreakfastItem;

namespace Breakfast.Api.Mapping
{
    /// <summary>
    /// Mapping for <see cref="BreakfastItem"/>.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class BreakfastItemProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastItemProfile"/> class.
        /// </summary>
        public BreakfastItemProfile()
        {
            CreateMap<CreateBreakfastItemRequest, BreakfastItem>();
            CreateMap<BreakfastItem, BreakfastItemViewModel>();
        }
    }
}
