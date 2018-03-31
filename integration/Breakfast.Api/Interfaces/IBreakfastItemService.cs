using System.Collections.Generic;
using System.Threading.Tasks;
using Breakfast.Api.Models.BreakfastItem;

namespace Breakfast.Api.Interfaces
{
    public interface IBreakfastItemService
    {
        /// <summary>
        /// Gets the breakfast item with the specified name. Or <c>null</c> if none exist.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        Task<BreakfastItemViewModel> GetByNameAsync(string name);

        /// <summary>
        /// Creates a new breakfast item according to the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<BreakfastItemViewModel> CreateAsync(CreateBreakfastItemRequest request);

        /// <summary>
        /// Gets all breakfast items.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<BreakfastItemViewModel>> GetAllAsync();

        /// <summary>
        /// Gets the breakfast item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<BreakfastItemViewModel> GetAsync(string id);

        /// <summary>
        /// Deletes the breakfast item with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task DeleteAsync(string id);
    }
}