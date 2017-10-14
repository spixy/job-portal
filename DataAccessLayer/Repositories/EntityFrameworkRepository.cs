using System;
using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using DataAccessLayer.Infrastructure;
using DataAccessLayer.Infrastructure.EntityFramework;

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

        public TEntity Add(TEntity entity)
        {
            return Context.Set<TEntity>().Add(entity);
        }

        public TEntity GetById(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public TEntity Delete(int id)
        {
            TEntity foundEntity = Context.Set<TEntity>().Find(id);
            return (foundEntity == null) ? null : this.Delete(foundEntity);
        }

        public TEntity Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            return Context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            TEntity foundEntity = Context.Set<TEntity>().Find(entity.Id);
            Context.Entry(foundEntity).CurrentValues.SetValues(entity);
        }
    }
}
