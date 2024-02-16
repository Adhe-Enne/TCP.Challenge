using Core.Abstractions;
using Core.Framework;
using TCP.Business.Extensions;
using TCP.Business.Interfaces;
using TCP.Model.Constants;
using TCP.Model.Entities;

namespace TCP.Business.Services
{
    public class ClientService : Service<Client>
    {
        readonly IValidatorStrategy<Client> _validatorStrategy;
        public ClientService(IRepository<Client> repository, IValidatorStrategy<Client> validator)
            : base(repository)
        {
            _validatorStrategy = validator;
        }

        public override IGenericResult Insert(Client entity)
        {
            bool cuitExists = _repository.AsQueryable().Any(x => x.CUIT == entity.CUIT);

            if (cuitExists)
                throw new TcpException(Messages.CUIT_EXISTS);

            _validatorStrategy.ValidateFields(entity);

            if (entity.IsDistributor())
                entity.Status = Model.Enums.MainStatus.DISABLED;

            _repository.Insert(entity);

            return new GenericResult(Messages.ENTITY_INSERTED);
        }

        public override IGenericResult Update(Client entity)
        {
            bool cuitExist = _repository.AsQueryable().Any( x =>x.CUIT == entity.CUIT && x.Id != entity.Id);

            if (cuitExist)
                throw new TcpException(Messages.CUIT_EXISTS);

            Client? toUpdate = _repository.Find(x => x.Id == entity.Id || x.CUIT == entity.CUIT);

            if (toUpdate is null)
                throw new TcpException(Messages.CLIENT_UNFOUND);

            if (toUpdate.Status == Model.Enums.MainStatus.DISABLED)
                throw new TcpException(Messages.CLIENT_INVALID);

            toUpdate.CompanyName = entity.CompanyName;
            toUpdate.Phone = entity.Phone;
            toUpdate.Email = entity.Email;
            toUpdate.Adress = entity.Adress;

            if (entity.IsDistributor())
                entity.Status = Model.Enums.MainStatus.DISABLED;

            _validatorStrategy.ValidateFields(entity);

            _repository.Update(toUpdate);
            return new GenericResult(Messages.ENTITY_UPDATED);
        }
    }
}