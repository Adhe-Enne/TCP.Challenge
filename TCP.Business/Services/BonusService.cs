using Core.Abstractions;
using Core.Framework;
using Dapper;
using Microsoft.EntityFrameworkCore;
using TCP.Business.Constants;
using TCP.Business.Enums;
using TCP.Business.Interfaces;
using TCP.Model.Entities;
using TCP.Model.Enums;
using TCP.Model.ViewSp;
using TCP.Repository.Interfaces;

namespace TCP.Business.Services
{
    /// <summary>
    /// Si las consultas tendieran a elevarse en complejidad, utilizamos este factory para dejar los controllers/services lo mas limpio posible.
    /// </summary>
    public class BonusService : IBonusService
    {
        readonly IRepositorySql<InvoiceClientBestSell> _repositorySql;
        readonly IRepository<Invoice> _repository;
        public BonusService(IRepository<Invoice> repository, IRepositorySql<InvoiceClientBestSell> repositorySql)
        {
            _repository = repository;
            _repositorySql = repositorySql;
        }

        public IQueryable<Invoice> GetQuery(QueryType queryType)
        {
            IQueryable<Invoice> entities = _repository.AsQueryable().Where(x => x.Status == MainStatus.ACTIVE)
                .Include(x => x.Client)
                .Include(x => x.Customer)
                .Include(x => x.Detail).ThenInclude(d => d.Product);

            return queryType switch
            {
                QueryType.QUERYONE => QueryOne(entities),
                QueryType.QUERYTWO => QueryTwo(entities),
                QueryType.QUERYTHREE => QueryThree(entities),
                _ => throw new TcpException(Model.Constants.Messages.QUERY_INVALID),
            };
        }

        private IQueryable<Invoice> QueryOne(IQueryable<Invoice> entities)
        {
            return entities.Where(x => x.TotalAmount > Constants.KeyBusiness.AMOUNT_INVOICED_TARGET);
        }

        private IQueryable<Invoice> QueryTwo(IQueryable<Invoice> entities)
        {
            return entities.Where(x => x.Client.CUIT.EndsWith(Constants.KeyBusiness.CUIT_END_TARGET));
        }

        private IQueryable<Invoice> QueryThree(IQueryable<Invoice> entities)
        {
            return entities.Where(x => x.Detail.Any(p => p.Product.Code == Constants.KeyBusiness.PRODUCT_TARGET));
        }


        public IEnumerable<InvoiceClientBestSell> GetSp(string datefrom, string dateto, int? id)
        {
            DynamicParameters parameters = new();
            parameters.Add(KeyBusiness.SP_PARAM_DATEFROM, datefrom);
            parameters.Add(KeyBusiness.SP_PARAM_DATETO, dateto);

            string query = KeyBusiness.SP_NAME_LIST;

            if (id is not null)
            {
                query = KeyBusiness.SP_NAME_ALONE;
                parameters.Add(KeyBusiness.SP_PARAM_CLIENTID, id);
            }

            return _repositorySql.ExecuteStoredProcedure(query, parameters);
        }
    }
}