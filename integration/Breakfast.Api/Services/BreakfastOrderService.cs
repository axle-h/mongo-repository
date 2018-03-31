using AutoMapper;
using Breakfast.Api.Data.Models;
using Breakfast.Api.Interfaces;
using Breakfast.Api.Models.BreakfastOrder;
using Breakfast.Api.Services.Crud;
using Microsoft.AspNetCore.Http;
using MongoDB.Extensions.Repository.Interfaces;

namespace Breakfast.Api.Services
{
    public class BreakfastOrderService : StandardMongoCrudService<BreakfastOrder, BreakfastOrderViewModel, CreateBreakfastOrderRequest>, IBreakfastOrderService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastOrderService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public BreakfastOrderService(IMongoRepository<BreakfastOrder> repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper, httpContextAccessor)
        {
        }
    }
}
