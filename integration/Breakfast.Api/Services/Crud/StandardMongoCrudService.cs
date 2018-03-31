using AutoMapper;
using Microsoft.AspNetCore.Http;
using MongoDB.Extensions.Repository.Interfaces;
using MongoDB.Extensions.Repository.Models;

namespace Breakfast.Api.Services.Crud
{
    public abstract class StandardMongoCrudService<TEntity, TViewModel, TCreateRequest> : MongoCrudService<IMongoRepository<TEntity>, TEntity, TViewModel, TCreateRequest>
        where TEntity : MongoEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardMongoCrudService{TEntity, TViewModel, TCreateRequest}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        protected StandardMongoCrudService(IMongoRepository<TEntity> repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper, httpContextAccessor)
        {
        }
    }
}