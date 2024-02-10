using Core.Abstractions;
using Core.Framework;
using FluentValidation;
using FluentValidation.Results;
using System.Linq.Expressions;
using TCP.Model.Constants;
using TCP.Model.Enums;
using TCP.Model.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TCP.Business.Services
{
    public class Service<T> : IService<T> where T : class, Core.Abstractions.IEntity
    {
        protected readonly IRepository<T> _repository;
        private IValidator<T> _validator;

        public Service(IRepository<T> repository, IValidator<T> validator)
        {
            this._repository = repository;
            this._validator = validator;
        }

        public T Find(int id)
        {
            return _repository.Find(x => x.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            return _repository.Filter(where, includeProperties);
        }

        public T Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            return _repository.Find(where, includeProperties);
        }

        public void Validate(T entity)
        {
            ValidationResult validationResult = _validator.Validate(entity);

            if (validationResult.IsValid)
                return;

            string message = string.Join("\n", validationResult.Errors.Select(x => x.ErrorMessage));

            throw new TcpException($"{Messages.ENTITY_ERROR_VALIDATE}: {message}");
        }

        public IGenericResult PhysicDelete(int id)
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
                ((IBusinessEntity)entity).Status = Model.Enums.MainStatus.DELETED;
            }
            _repository.Update(entity);

            return new GenericResult(Model.Constants.Messages.ENTITY_DELETED);
        }
    }
}