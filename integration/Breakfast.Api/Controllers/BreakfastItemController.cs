using System.Collections.Generic;
using System.Threading.Tasks;
using Breakfast.Api.Infrastructure;
using Breakfast.Api.Interfaces;
using Breakfast.Api.Models;
using Breakfast.Api.Models.BreakfastItem;
using Microsoft.AspNetCore.Mvc;

namespace Breakfast.Api.Controllers
{
    [ApiRoute]
    public class BreakfastItemController : Controller
    {
        private readonly IBreakfastItemService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastItemController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public BreakfastItemController(IBreakfastItemService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates a new breakfast item according to the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public async Task<BreakfastItemViewModel> CreateBreakfastItemAsync([FromBody] CreateBreakfastItemRequest request) => await _service.CreateAsync(request);

        /// <summary>
        /// Gets the breakfast item with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [HttpGet("by-name/{name}")]
        public async Task<BreakfastItemViewModel> GetBreakfastItemByNameAsync(string name) => await _service.GetByNameAsync(name);

        /// <summary>
        /// Gets the breakfast item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<BreakfastItemViewModel> GetBreakfastItemAsync(string id) => await _service.GetAsync(id);

        /// <summary>
        /// Gets all breakfast items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICollection<BreakfastItemViewModel>> GetAllBreakfastItemsAsync() => await _service.GetAllAsync();

        /// <summary>
        /// Deletes the breakfast item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteBreakfastItemAsync(string id) => await _service.DeleteAsync(id);
    }
}
