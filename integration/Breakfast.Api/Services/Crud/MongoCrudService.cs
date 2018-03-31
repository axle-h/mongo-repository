using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Breakfast.Api.Infrastructure.Contracts;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using MongoDB.Extensions.Repository.Interfaces;
using MongoDB.Extensions.Repository.Models;

namespace Breakfast.Api.Services.Crud
{
    public abstract class MongoCrudService<TRepository, TEntity, TViewModel, TCreateRequest>
        where TEntity : MongoEntity
        where TRepository : IMongoRepository<TEntity>
    {
        protected readonly TRepository Repository;
        protected readonly IMapper Mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoCrudService{TRepository, TEntity, TViewModel, TCreateRequest}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        protected MongoCrudService(TRepository repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            Repository = repository;
            Mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Creates a new <see cref="TViewModel"/> according to the specified <see cref="TCreateRequest"/>.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<TViewModel> CreateAsync(TCreateRequest request)
        {
            var item = Mapper.Map<TEntity>(request);

            try
            {
                await Repository.AddAsync(item, RequestAborted);
            }
            catch (MongoWriteException mre)
            {
                HandleOnCreate(mre, request);
                throw;
            }

            return Mapper.Map<TViewModel>(item);
        }

        /// <summary>
        /// Gets all <see cref="TViewModel"/>.
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<TViewModel>> GetAllAsync()
        {
            var items = await Repository.GetAllAsync(RequestAborted);
            return Mapper.Map<ICollection<TViewModel>>(items);
        }

        /// <summary>
        /// Gets the <see cref="TViewModel"/> with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<TViewModel> GetAsync(string id)
        {
            var item = await Repository.GetAsync(id, RequestAborted);
            if (item == null)
            {
                throw EntityNotFoundException.Create<TEntity>(x => x.Id, id);
            }

            return Mapper.Map<TViewModel>(item);
        }

        /// <summary>
        /// Deletes the <see cref="TViewModel"/> with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task DeleteAsync(string id)
        {
            if (!await Repository.DeleteAsync(id, RequestAborted))
            {
                throw EntityNotFoundException.Create<TEntity>(x => x.Id, id);
            }
        }

        /// <summary>
        /// Handles a mongo write exception that was thrown when attempting to create an entity.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="request">The request.</param>
        protected virtual void HandleOnCreate(MongoWriteException exception, TCreateRequest request)
        {
        }

        /// <summary>
        /// Gets the request aborted cancellation token.
        /// </summary>
        protected CancellationToken RequestAborted => _httpContextAccessor.HttpContext?.RequestAborted ?? CancellationToken.None;
    }
}
