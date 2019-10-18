using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Interface.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
