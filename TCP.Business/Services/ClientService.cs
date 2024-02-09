using Core.Abstractions;
using Core.Framework;
using FluentValidation;
using FluentValidation.Results;
using TCP.Model.Entities;
using TCP.Model.Constants;

namespace TCP.Business.Services
{
    public class ClientService : Service<Client>, IServiceCrud<Client>
    {
        private IValidator<Client> _validator;
        public ClientService(IRepository<Client> repository, IValidator<Client> validator) : base(repository, validator)
        {
            _validator = validator;
        }

        public IGenericResult LogicDelete(int id)
        {
            Client? entity = this.Find(id);

            entity.Status = Model.MainStatus.DELETED;
            _repository.Update(entity);

            return new GenericResult(Model.Constants.Messages.ENTITY_DELETED);
        }

        public IGenericResult PhysicDelete(int id)
        {
            Client? entity = this.Find(id);
            _repository.Delete(entity);

            return new GenericResult(Model.Constants.Messages.ENTITY_DELETED_PERMANETLY);
        }

        public IGenericResult Insert(Client entity)
        {
            Validate(entity);

            bool cuitExists = _repository.AsQueryable().Any(x=> x.CUIT == entity.CUIT);

            if (cuitExists)
                throw new TcpException(Messages.CUIT_EXISTS);

            return new GenericResult();
        }

        public IGenericResult Update(Client entity)
        {
            this.Validate(entity);

            Client toUpdate = GetValid(entity);

            toUpdate.CompanyName = entity.CompanyName;
            toUpdate.Adress = entity.Adress;

            if (entity.CompanyName.StartsWith(Constants.KeyName.DISTRIBUTOR))
                toUpdate.Disabled = true;

            _repository.Update(toUpdate);
            return new GenericResult();
        }

        private Client GetValid(Client entity)
        {
            Client? existed = _repository.Find(x => x.Id == entity.Id || x.CUIT == entity.CUIT);
            
            if (existed is null)
                throw new TcpException(Messages.ENTITY_UNFOUND);

            return entity;
        }
    }
}
