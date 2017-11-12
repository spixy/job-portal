using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Filters;

namespace BusinessLayer.Facades.JobApplication
{
    public interface IJobApplicationFacade
    {
        Task<int> Create(JobApplicationCreateDto dto);
        void Delete(int id);
        Task Get(int id);
        Task Get(JobApplicationFilterDto dto);
        Task Update(JobApplicationDto dto);
    }
}