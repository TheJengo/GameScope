using GameScope.Domain.Interfaces;
using GameScope.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GameScope.Infra.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly GameScopeContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(GameScopeContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list;

            IQueryable<TEntity> dbQuery = DbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty).AsNoTracking();

            list = dbQuery.AsNoTracking().ToList<TEntity>();

            return list;
        }

        public virtual IList<TEntity> GetList(Func<TEntity, bool> where,
    params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list;

            IQueryable<TEntity> dbQuery = DbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty).AsNoTracking();

            list = dbQuery.AsNoTracking().Where(where).ToList<TEntity>();

            return list;
        }

        public virtual TEntity GetSingle(Func<TEntity, bool> where,
    params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            TEntity item = null;

            IQueryable<TEntity> dbQuery = DbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty).AsNoTracking();

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause

            return item;
        }

        public void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public TEntity GetById(int id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public void Remove(int id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void Remove(TEntity obj)
        {
            DbSet.Remove(obj);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
