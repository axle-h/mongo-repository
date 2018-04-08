using System.Collections.Generic;
using System.Threading.Tasks;
using Breakfast.Api.Infrastructure;
using Breakfast.Api.Interfaces;
using Breakfast.Api.Models.BreakfastReview;
using Microsoft.AspNetCore.Mvc;

namespace Breakfast.Api.Controllers
{
    [ApiRoute]
    public class BreakfastReviewController : Controller
    {
        private readonly IBreakfastReviewService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastReviewController"/> class.
        /// </summary>
        /// <param name="service">The breakfast review service.</param>
        public BreakfastReviewController(IBreakfastReviewService service)
        {
            _service = service;
        }

        /// <summary>
        /// Creates a new breakfast review according to the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        public async Task<BreakfastReviewViewModel> CreateBreakfastReviewAsync([FromBody] CreateBreakfastReviewRequest request) => await _service.CreateAsync(request);

        /// <summary>
        /// Gets the breakfast review with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<BreakfastReviewViewModel> GetBreakfastReviewAsync(string id) => await _service.GetAsync(id);

        /// <summary>
        /// Gets all breakfast reviews.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ICollection<BreakfastReviewViewModel>> GetAllBreakfastReviewsAsync() => await _service.GetAllAsync();

        /// <summary>
        /// Deletes the breakfast review with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task DeleteBreakfastReviewAsync(string id) => await _service.DeleteAsync(id);

        /// <summary>
        /// Un-deletes the breakfast review with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpPut("{id}/un-delete")]
        public async Task UnDeleteBreakfastReviewAsync(string id) => await _service.UnDeleteAsync(id);

        /// <summary>
        /// Updates the breakfast review with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPut("{id}/rating")]
        [ValidateModel]
        public async Task<BreakfastReviewViewModel> UpdateAsync(string id, [FromBody] UpdateBreakfastReviewRatingRequest request) =>
            await _service.UpdateRatingAsync(id, request);
    }
}
