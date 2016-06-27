using System;
using System.Data.Entity;
using System.Linq;
using SportLife.Core.Interfaces;

namespace SportLife.Core.Generic {
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class {
        protected readonly DbContext Context;

        public virtual string TableName => string.Empty;

        public GenericRepository ( DbContext dbContext ) {
            Context = dbContext;
        }

        public TEntity Get ( int id ) {
            return Context.Set<TEntity>().Find(id);
        }

        public System.Collections.Generic.IEnumerable<TEntity> GetAll () {
            return Context.Set<TEntity>().ToList();
        }

        public System.Collections.Generic.IEnumerable<TEntity> Find ( System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate ) {
            return Context.Set<TEntity>().Where(predicate);
        }

        public void Add ( TEntity entity ) {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange ( System.Collections.Generic.IEnumerable<TEntity> entities ) {
            Context.Set<TEntity>().AddRange(entities);
        } 

        public void Remove ( TEntity entity ) {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange ( System.Collections.Generic.IEnumerable<TEntity> entities ) {
            Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
