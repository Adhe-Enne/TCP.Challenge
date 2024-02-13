using Core.Abstractions;
using Core.Framework;
using System.Linq.Expressions;
using TCP.Model.Interfaces;

namespace TCP.Business.Services
{
    public class Service<T> : IService<T> where T : class, Core.Abstractions.IEntity
    {
        protected readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            this._repository = repository;
        }

        public T Find(int id)
        {
            return _repository.Find(x => x.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.AsQueryable();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            return _repository.Filter(where, includeProperties);
        }

        public T Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            return _repository.Find(where, includeProperties);
        }

        public virtual IGenericResult PhysicDelete(int id)
        {
            T entity = this.Find(id);

            _repository.Delete(entity);

            return new GenericResult(Model.Constants.Messages.ENTITY_DELETED_PERMANETLY);
        }

        public virtual IGenericResult LogicDelete(int id)
        {
            T? entity = this.Find(id);

            if (entity is null)
                throw new TcpException(Model.Constants.Messages.ENTITY_UNFOUND);

            if (entity is IBusinessEntity)
            {
                ((IBusinessEntity)entity).Status = Model.Enums.MainStatus.DISABLED;
            }

            _repository.Update(entity);

            return new GenericResult(Model.Constants.Messages.ENTITY_DELETED);
        }

        public virtual IGenericResult Insert(T model)
        {
            _repository.Update(model);
            return new GenericResult(Model.Constants.Messages.ENTITY_INSERTED);
        }

        public virtual IGenericResult Update(T model)
        {
            _repository.Update(model);
            return new GenericResult(Model.Constants.Messages.ENTITY_UPDATED);
        }

        public virtual IQueryable<T> AsQueryable()
        {
            return _repository.AsQueryable();
        }
    }
}