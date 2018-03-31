using System.Threading.Tasks;
using AutoMapper;
using Breakfast.Api.Data.Models;
using Breakfast.Api.Interfaces;
using Breakfast.Api.Models.BreakfastReview;
using Breakfast.Api.Services.Crud;
using Microsoft.AspNetCore.Http;
using MongoDB.Extensions.Repository.Interfaces;

namespace Breakfast.Api.Services
{
    public class BreakfastReviewService : StandardSoftDeletableMongoCrudService<BreakfastReview, BreakfastReviewViewModel, CreateBreakfastReviewRequest>, IBreakfastReviewService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BreakfastReviewService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public BreakfastReviewService(ISoftDeletableMongoRepository<BreakfastReview> repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper, httpContextAccessor)
        {
        }
    }
}
