using Core.Abstractions;
using Core.Framework;
using FluentValidation;
using TCP.Model.Entities;

namespace TCP.Business.Services
{
    /// <summary>
    /// Crud Service Base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceCrud<T> : Service<T>, IServiceCrud<T> where T : class, Core.Abstractions.IEntity
    {
        protected readonly IRepository<T> _repository;
        private IValidator<T> _validator;

        public ServiceCrud(IRepository<T> repository, IValidator<T> validator) : base(repository, validator)
        {
            this._repository = repository;
            this._validator = validator;
        }

        public virtual IGenericResult Insert(T model)
        {
            throw new NotImplementedException();
        }

        public virtual IGenericResult LogicDelete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual IGenericResult PhysicDelete(int id)
        {
                T entity = this.Find(id);
            if (entity is null)
                throw new TcpException(Model.Constants.Messages.ENTITY_UNFOUND);

            _repository.Delete(entity);

            return new GenericResult(Model.Constants.Messages.ENTITY_DELETED_PERMANETLY);
        }

        public virtual IGenericResult Update(T model)
        {
            throw new NotImplementedException();
        }
    }
}
