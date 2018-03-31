using System.Collections.Generic;
using System.Threading.Tasks;
using Breakfast.Api.Models.BreakfastOrder;

namespace Breakfast.Api.Interfaces
{
    public interface IBreakfastOrderService
    {
        /// <summary>
        /// Gets all breakfast orders.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<BreakfastOrderViewModel>> GetAllAsync();

        /// <summary>
        /// Creates a new breakfast order according to the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<BreakfastOrderViewModel> CreateAsync(CreateBreakfastOrderRequest request);
    }
}