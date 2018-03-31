using System.Threading.Tasks;
using AutoMapper;
using Breakfast.Api.Data.Interfaces;
using Breakfast.Api.Data.Models;
using Breakfast.Api.Infrastructure.Contracts;
using Breakfast.Api.Interfaces;
using Breakfast.Api.Models.BreakfastItem;
using Breakfast.Api.Services.Crud;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace Breakfast.Api.Services
{
    public class BreakfastItemService : MongoCrudService<IBreakfastItemRepository, BreakfastItem, BreakfastItemViewModel, CreateBreakfastItemRequest>, IBreakfastItemService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastItemService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public BreakfastItemService(IBreakfastItemRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper, httpContextAccessor)
        {
        }

        /// <summary>
        /// Gets the breakfast item with the specified name. Or <c>null</c> if none exist.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public async Task<BreakfastItemViewModel> GetByNameAsync(string name)
        {
            var item = await Repository.GetByNameAsync(name, RequestAborted);
            if (item == null)
            {
                throw EntityNotFoundException.Create<BreakfastItem>(x => x.Name, name);
            }

            return Mapper.Map<BreakfastItemViewModel>(item);
        }

        /// <summary>
        /// Handles a mongo write exception that was thrown when attempting to create an entity.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="request">The request.</param>
        protected override void HandleOnCreate(MongoWriteException exception, CreateBreakfastItemRequest request)
        {
            if (exception.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                throw BadRequestException.Duplicate<BreakfastItem>(x => x.Name, request.Name, exception);
            }
        }
    }
}
