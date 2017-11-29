using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace DataAccessLayer.Repositories
{
    public class JobOfferRepository : EfRepository<JobOffer>, IJobOfferRepository
    {
        public JobOfferRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public async Task<List<JobOffer>> GetBySkill(Skill skill)
        {
            var foundSkill = await Context.Set<Skill>().FindAsync(skill.Id);
            return foundSkill?.JobOffers;
        }
    }
}
