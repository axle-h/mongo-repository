using System.Collections.Generic;
using System.Threading.Tasks;
using Breakfast.Api.Infrastructure;
using Breakfast.Api.Interfaces;
using Breakfast.Api.Models.BreakfastOrder;
using Microsoft.AspNetCore.Mvc;

namespace Breakfast.Api.Controllers
{
    [ApiRoute]
    public class BreakfastOrderController : Controller
    {
        private readonly IBreakfastOrderService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastOrderController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public BreakfastOrderController(IBreakfastOrderService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates a new breakfast order according to the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public async Task<BreakfastOrderViewModel> CreateBreakfastOrderAsync([FromBody] CreateBreakfastOrderRequest request) => await _service.CreateAsync(request);

        /// <summary>
        /// Gets all breakfast orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICollection<BreakfastOrderViewModel>> GetAllBreakfastOrdersAsync() => await _service.GetAllAsync();


    }
}
