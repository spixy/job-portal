using System.Linq;
using DataAccessLayer.Entities;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace DataAccessLayer.Repositories
{
    public class SkillRepository : EfRepository<Skill>, ISkillRepository
    {
        public SkillRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public Skill GetByName(string name)
        {
            return Context.Set<Skill>().SingleOrDefault(tag => tag.Name.Equals(name));
        }
    }
}
