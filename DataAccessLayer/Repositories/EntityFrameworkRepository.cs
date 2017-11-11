using System;
using System.Threading.Tasks;
using DataAccessLayer.Contexts;
using Infrastructure;

namespace DataAccessLayer.Repositories
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IUnitOfWorkProvider provider;

        public EntityFrameworkRepository(IUnitOfWorkProvider provider)
        {
            this.provider = provider;
        }

        protected JobPortalDbContext Context => ((EntityFrameworkUnitOfWork)provider.GetUnitOfWorkInstance()).Context;

        public TEntity Create(TEntity entity)
        {
            return Context.Set<TEntity>().Add(entity);
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public Task<TEntity> GetAsync(int id)
        {
            return this.Context.Set<TEntity>().FindAsync(id);
        }

        public TEntity Delete(int id)
        {
	        TEntity foundEntity = this.Get(id);
			if (foundEntity == null)
	        {
		        throw new Exception("ID " + id + " not found.");
	        }
			return Context.Set<TEntity>().Remove(foundEntity);
        }

        public void Update(TEntity entity)
        {
            TEntity foundEntity = this.Get(entity.Id);
            Context.Entry(foundEntity).CurrentValues.SetValues(entity);
        }
    }
}
