
using System.Threading.Tasks;

namespace Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity Create(TEntity entity);

        TEntity Get(int id);
        Task<TEntity> GetAsync(int id);

        TEntity Delete(int id);

        void Update(TEntity entity);
    }
}
