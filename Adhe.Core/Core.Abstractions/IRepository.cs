using System.Linq.Expressions;

namespace Core.Abstractions
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();
        T? Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> Filter(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        void Delete(T entity);
        void Insert(T entity);
        void Update(T entity);
    }
}