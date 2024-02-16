using System.Linq.Expressions;

namespace Core.Abstractions
{
    /// <summary>
    /// Servicio Base, con acceso e insercion de registros de manera generica.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IService<T>
    {
        IEnumerable<T> Filter(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetAll();
        T Find(int id);
        T Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> AsQueryable();
        IGenericResult Insert(T model);
        IGenericResult Update(T model);
        IGenericResult LogicDelete(int id);
        IGenericResult PhysicDelete(int id);
    }
}