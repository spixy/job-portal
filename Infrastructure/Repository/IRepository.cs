using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity Create(TEntity entity);

        Task<TEntity> GetAsync(int id);
        Task<TEntity> GetAsync(int id, params string[] includes);

        TEntity Delete(int id);

        void Update(TEntity entity);
    }
}
