using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs.Common;
using BusinessLayer.QueryObjects;
using Infrastructure;
using Infrastructure.Query;

namespace BusinessLayer.Services.Common
{
    public abstract class CrudQueryServiceBase<TEntity, TDto, TFilterDto> : ServiceBase
        where TFilterDto : FilterDtoBase, new()
        where TEntity : class, IEntity, new()
        where TDto : DtoBase
    {
        protected readonly IRepository<TEntity> repository;

        protected readonly QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>> query;

        protected CrudQueryServiceBase(IMapper mapper, IRepository<TEntity> repository, QueryObjectBase<TDto, TEntity, TFilterDto, IQuery<TEntity>> query) : base(mapper)
        {
            this.query = query;
            this.repository = repository;
        }

        /// <summary>
        /// Gets DTO representing the entity according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        public virtual async Task<TDto> GetAsync(int entityId, bool withIncludes = true)
        {
            TEntity entity;
            if (withIncludes)
            {
                entity = await GetWithIncludesAsync(entityId);
            }
            else
            {
                entity = await this.repository.GetAsync(entityId);
            }
            return entity != null ? this.mapper.Map<TDto>(entity) : null;
        }

        /// <summary>
        /// Gets entity (with complex types) according to ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <returns>The DTO representing the entity</returns>
        protected abstract Task<TEntity> GetWithIncludesAsync(int entityId);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        public virtual int Create(TDto entityDto)
        {
            TEntity entity = this.mapper.Map<TEntity>(entityDto);
            this.repository.Create(entity);
            return entity.Id;
        }

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        public virtual async Task Update(TDto entityDto)
        {
            TEntity entity = await GetWithIncludesAsync(entityDto.Id);
            this.mapper.Map(entityDto, entity);
            this.repository.Update(entity);
        }

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        public virtual void Delete(int entityId)
        {
            this.repository.Delete(entityId);
        }

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        public virtual async Task<QueryResultDto<TDto, TFilterDto>> ListAllAsync()
        {
            return await this.query.ExecuteQuery(new TFilterDto());
        }
    }
}
