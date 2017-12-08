using DataAccessLayer.Entities;
using Infrastructure.Repository;

namespace DataAccessLayer.Repositories
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Skill GetByName(string name);
    }
}
