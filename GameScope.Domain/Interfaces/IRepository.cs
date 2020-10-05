using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GameScope.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);
        IList<TEntity> GetList(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties);
        TEntity GetSingle(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties);
        void Add(TEntity obj);
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        void Remove(int id);
        void Remove(TEntity obj);
        int SaveChanges();
        void Update(TEntity obj);
    }
}
