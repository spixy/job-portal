using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Infrastructure.UnitOfWork;

namespace Infrastructure.Repository
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly IUnitOfWorkProvider provider;

        protected DbContext Context => ((EfUnitOfWork) provider.GetUnitOfWorkInstance()).Context;

        public EfRepository(IUnitOfWorkProvider provider)
        {
            this.provider = provider;
        }

        public TEntity Create(TEntity entity)
        {
            return Context.Set<TEntity>().Add(entity);
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await this.Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(int id, params string[] includes)
        {
            DbQuery<TEntity> ctx = Context.Set<TEntity>();
            foreach (string include in includes)
            {
                ctx = ctx.Include(include);
            }
            return await ctx.SingleOrDefaultAsync(entity => entity.Id == id);
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
