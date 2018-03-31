using System.Threading.Tasks;
using AutoMapper;
using Breakfast.Api.Infrastructure.Contracts;
using Microsoft.AspNetCore.Http;
using MongoDB.Extensions.Repository.Interfaces;
using MongoDB.Extensions.Repository.Models;

namespace Breakfast.Api.Services.Crud
{
    public abstract class SoftDeletableMongoCrudService<TRepository, TEntity, TViewModel, TCreateRequest> : MongoCrudService<TRepository, TEntity, TViewModel, TCreateRequest>
        where TEntity : SoftDeletableMongoEntity
        where TRepository : ISoftDeletableMongoRepository<TEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoftDeletableMongoCrudService{TRepository, TEntity, TViewModel, TCreateRequest}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        protected SoftDeletableMongoCrudService(TRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper, httpContextAccessor)
        {
        }

        /// <summary>
        /// Un-deletes the <see cref="TViewModel"/> with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task UnDeleteAsync(string id)
        {
            if (!await Repository.UnDeleteAsync(id, RequestAborted))
            {
                throw EntityNotFoundException.Create<TEntity>(x => x.Id, id);
            }
        }
    }
}