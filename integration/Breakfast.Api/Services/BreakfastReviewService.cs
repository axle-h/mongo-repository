using System.Threading.Tasks;
using AutoMapper;
using Breakfast.Api.Data.Interfaces;
using Breakfast.Api.Data.Models;
using Breakfast.Api.Infrastructure.Contracts;
using Breakfast.Api.Interfaces;
using Breakfast.Api.Models.BreakfastReview;
using Breakfast.Api.Services.Crud;
using Microsoft.AspNetCore.Http;
using MongoDB.Extensions.Repository.Interfaces;

namespace Breakfast.Api.Services
{
    public class BreakfastReviewService : SoftDeletableMongoCrudService<IBreakfastReviewRepository, BreakfastReview, BreakfastReviewViewModel, CreateBreakfastReviewRequest>, IBreakfastReviewService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastReviewService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public BreakfastReviewService(IBreakfastReviewRepository repository,
                                      IMapper mapper,
                                      IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper, httpContextAccessor)
        {
        }

        /// <summary>
        /// Updates the breakfast review with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<BreakfastReviewViewModel> UpdateRatingAsync(string id, UpdateBreakfastReviewRatingRequest request)
        {
            var result = await Repository.UpdateRatingAsync(id, request.Rating);
            if (result == null)
            {
                throw EntityNotFoundException.Create<BreakfastReview>(x => x.Id, id);
            }
            return Mapper.Map<BreakfastReviewViewModel>(result);
        }
    }
}
