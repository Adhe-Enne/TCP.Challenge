using Core.Abstractions;
using Core.Framework;
using FluentValidation;
using FluentValidation.Results;
using TCP.Model.Entities;
using TCP.Model.Constants;
using TCP.Model.Enums;

namespace TCP.Business.Services
{
    public class ClientService : Service<Client>, IServiceCrud<Client>
    {
        public ClientService(IRepository<Client> repository, IValidator<Client> validator)
            : base(repository, validator)
        {
        }

        public IGenericResult Insert(Client entity)
        {
            bool cuitExists = _repository.AsQueryable().Any(x=> x.CUIT == entity.CUIT);

            if (cuitExists)
                throw new TcpException(Messages.CUIT_EXISTS);

            Validate(entity);
            _repository.Insert(entity);

            return new GenericResult(Messages.ENTITY_INSERTED);
        }

        public IGenericResult Update(Client entity)
        {
            bool cuitExist = _repository.AsQueryable().Any(x => x.CUIT == entity.CUIT && x.Id != entity.Id);

            if (cuitExist)
                throw new TcpException(Messages.CUIT_EXISTS);

            Client? toUpdate = _repository.Find(x => x.Id == entity.Id || x.CUIT == entity.CUIT);

            if (toUpdate is null)
                throw new TcpException(Messages.CLIENT_UNFOUND);

            toUpdate.CompanyName = entity.CompanyName;
            toUpdate.Adress = entity.Adress;

            if (entity.CompanyName.StartsWith(Constants.KeyName.DISTRIBUTOR))
                toUpdate.Disabled = true;

            this.Validate(entity);

            _repository.Update(toUpdate);
            return new GenericResult(Messages.ENTITY_UPDATED);
        }
    }
}
