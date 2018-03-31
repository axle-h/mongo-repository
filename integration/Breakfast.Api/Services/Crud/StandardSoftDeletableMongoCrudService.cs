using AutoMapper;
using Microsoft.AspNetCore.Http;
using MongoDB.Extensions.Repository.Interfaces;
using MongoDB.Extensions.Repository.Models;

namespace Breakfast.Api.Services.Crud
{
    public abstract class StandardSoftDeletableMongoCrudService<TEntity, TViewModel, TCreateRequest> : SoftDeletableMongoCrudService<ISoftDeletableMongoRepository<TEntity>, TEntity, TViewModel, TCreateRequest>
        where TEntity : SoftDeletableMongoEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardSoftDeletableMongoCrudService{TEntity, TViewModel, TCreateRequest}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        protected StandardSoftDeletableMongoCrudService(ISoftDeletableMongoRepository<TEntity> repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper, httpContextAccessor)
        {
        }
    }
}