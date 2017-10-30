﻿
namespace Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity Add(TEntity entity);
        TEntity GetById(int id);
        TEntity Delete(int id);
        void Update(TEntity entity);
    }
}