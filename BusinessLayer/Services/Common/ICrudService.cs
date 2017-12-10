using System.Threading.Tasks;
using BusinessLayer.DTOs.Common;

namespace BusinessLayer.Services.Common
{
	public interface ICrudService<TDto, TFilterDto> : ICrudService<TDto, TDto, TFilterDto>
		where TFilterDto : FilterDtoBase, new()
		where TDto : DtoBase
	{
	}

	public interface ICrudService<TDto, TUpdateDto, TFilterDto>
        where TFilterDto : FilterDtoBase, new()
        where TDto : DtoBase
		where TUpdateDto : DtoBase
	{
        /// <summary>
        ///  Gets user with given ID
        /// </summary>
        /// <param name="entityId">entity ID</param>
        /// <param name="withIncludes">include all entity complex types</param>
        /// <returns>The DTO representing the entity</returns>
        Task<TDto> GetAsync(int entityId, bool withIncludes = true);

        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        TDto Create(TDto entityDto);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityDto">entity details</param>
        Task Update(TUpdateDto entityDto);

        /// <summary>
        /// Deletes entity with given Id
        /// </summary>
        /// <param name="entityId">Id of the entity to delete</param>
        void Delete(int entityId);

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<TDto, TFilterDto>> ListAllAsync();

        /// <summary>
        /// Gets all DTOs (for given type)
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>all available dtos (for given type)</returns>
        Task<QueryResultDto<TDto, TFilterDto>> ListAllAsync(TFilterDto filter);
    }
}
