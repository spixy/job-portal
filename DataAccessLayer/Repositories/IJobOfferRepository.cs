using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Infrastructure.Repository;

namespace DataAccessLayer.Repositories
{
    public interface IJobOfferRepository : IRepository<JobOffer>
    {
        Task<List<JobOffer>> GetBySkill(Skill skill);
    }
}
