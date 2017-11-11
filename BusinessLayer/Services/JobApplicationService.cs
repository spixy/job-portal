using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DTOs;
using BusinessLayer.Services.Common;
using DataAccessLayer.Entities;
using Infrastructure;

namespace BusinessLayer.Services
{
    public class JobApplicationService : ServiceBase
    {
        private readonly IRepository<JobApplication> applicationRepository;
        private readonly IRepository<JobCandidate> candidateRepository;
        private readonly IRepository<JobOffer> offerRepository;
        private readonly IRepository<Answer> answerRepository;

        public JobApplicationService(IMapper mapper, IRepository<JobApplication> applicationRepository, IRepository<JobCandidate> candidateRepository, IRepository<JobOffer> offerRepository, IRepository<Answer> answerRepository) : base(mapper)
        {
            this.applicationRepository = applicationRepository;
            this.candidateRepository = candidateRepository;
            this.offerRepository = offerRepository;
            this.answerRepository = answerRepository;
        }

        public async Task<int> Create(JobApplicationCreateDto dto)
        {
            JobApplication application = this.mapper.Map<JobApplication>(dto.applicationDto);

            JobOffer offer = await this.offerRepository.GetAsync(dto.applicationDto.JobOfferId);
            JobCandidate candidate = await this.candidateRepository.GetAsync(dto.applicationDto.JobCandidateId);

            // unregistered user?
            if (candidate == null && dto.candidateDto != null)
            {
                candidate = this.mapper.Map<JobCandidate>(dto.candidateDto);
                this.candidateRepository.Create(candidate);
            }

            application.JobCandidate = candidate;
            application.JobOffer = offer;
            this.applicationRepository.Create(application);

            return application.Id;
        }

        public void Remove(int id)
        {
            /*JobApplication jobApplication = await this.applicationRepository.GetAsync(id);
            List<Answer> answers = jobApplication != null ? jobApplication.Answers : null;*/

            this.applicationRepository.Delete(id);

            /*if (answers != null)
            {
                foreach (Answer answer in answers)
                {
                    this.answerRepository.GetAsync(answer);
                }
            }*/
        }
    }
}
