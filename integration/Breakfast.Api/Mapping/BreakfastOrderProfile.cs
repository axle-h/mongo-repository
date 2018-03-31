using AutoMapper;
using Breakfast.Api.Data.Models;
using Breakfast.Api.Models.BreakfastOrder;

namespace Breakfast.Api.Mapping
{
    /// <summary>
    /// Mappings for <see cref="BreakfastOrder"/>.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class BreakfastOrderProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastOrderProfile"/> class.
        /// </summary>
        public BreakfastOrderProfile()
        {
            CreateMap<BreakfastOrder, BreakfastOrderViewModel>();
            CreateMap<CreateBreakfastOrderRequest, BreakfastOrder>();
        }
    }
}
