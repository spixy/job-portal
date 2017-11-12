using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;
using BusinessLayer.Services.Common;

namespace BusinessLayer.Services.Answers
{
    public interface IAnswerService : ICrudService<AnswerDto, AnswerFilterDto>
    {
        /// <summary>
        /// Find all answers for given question
        /// </summary>
        /// <param name="questionId">questionId</param>
        /// <returns>Answers for given question</returns>
        Task<IEnumerable<AnswerDto>> GetByQuestion(int questionId);

        /// <summary>
        /// Find all answers for given application
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>Answers for given application</returns>
        Task<IEnumerable<AnswerDto>> GetByApplication(int applicationId);

        /// <summary>
        /// Find answers for given question and application
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <param name="questionId">questionId</param>
        /// <returns>Answers for given question and application</returns>
        Task<AnswerDto> GetByApplicationQuestion(int applicationId, int questionId);
    }
}
