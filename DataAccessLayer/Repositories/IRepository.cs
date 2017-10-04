using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories
{
    internal interface IRepository<T> where T: IEntity
    {
        T Add(T entity);
        T GetById(int id);
        bool Remove(T entity);
        bool Update(T entity);
    }
}
