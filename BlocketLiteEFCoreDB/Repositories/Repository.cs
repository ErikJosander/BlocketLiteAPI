using BlocketLiteEFCoreDB.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace BlocketLiteEFCoreDB.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _contex;

        public Repository(DbContext context)
        {
            _contex = context ?? throw new ArgumentNullException(nameof(context));
        }

        public TEntity Get(int id)
        {
            Debug.WriteLine("****************** _contex.Set<TEntity>().Find(id) = " + _contex.Set<TEntity>().Find(id));
            return _contex.Set<TEntity>().Find(id); 
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _contex.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _contex.Set<TEntity>().Where(expression);
        }

        public void Add(TEntity entity)
        {
            _contex.Set<TEntity>().Add(entity);
        }

        public void AddRage(IEnumerable<TEntity> entities)
        {
            _contex.Set<IEnumerable<TEntity>>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _contex.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _contex.Set<IEnumerable<TEntity>>().RemoveRange(entities);
        }

        public void Save()
        {
            _contex.SaveChanges();
        }
    }
}
