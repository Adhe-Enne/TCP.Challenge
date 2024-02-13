using Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TCP.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TCP.DataBaseContext.DataBaseContext _ctx;

        public Repository(TCP.DataBaseContext.DataBaseContext ctx)
        {
            this._ctx = ctx;
        }

        private static IQueryable<T> PerformInclusions(IEnumerable<Expression<Func<T, object>>> includeProperties,
                                                       IQueryable<T> query)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        #region IRepository<T> Members

        public IQueryable<T> AsQueryable()
        {
            return _ctx.Set<T>().AsQueryable();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = AsQueryable();
            query = PerformInclusions(includeProperties, query);
            return query.Where(where);
        }

        public T Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = AsQueryable();

            query = PerformInclusions(includeProperties, query);

            return query.FirstOrDefault(where);
        }

        public void Delete(T entity)
        {
            _ctx.Set<T>().Remove(entity);

            _ctx.SaveChanges();
        }

        public void Insert(T entity)
        {
            DateTime timestamp = DateTime.Now;

            if (entity is IDatetimeManaged audit)
            {
                audit.DateAdded = timestamp;
                audit.DateUpdated = timestamp;
            }

            _ctx.Set<T>().Add(entity);
            _ctx.SaveChanges();
        }

        public void Update(T entity)
        {
            DateTime timestamp = DateTime.Now;

            if (entity is IDatetimeManaged audit)
            {
                audit.DateUpdated = timestamp;
            }

            _ctx.Set<T>().Attach(entity);
            _ctx.Entry(entity).State = EntityState.Modified;

            _ctx.SaveChanges();
        }
        #endregion
    }
}