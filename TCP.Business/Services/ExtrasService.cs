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

using Address = Invoicer.Models.Address;
using DetailRow = Invoicer.Models.DetailRow;
using InvoicerPdf = Invoicer.Services.InvoicerApi;
using ItemRow = Invoicer.Models.ItemRow;
using OrientationOption = Invoicer.Models.OrientationOption;
using SizeOption = Invoicer.Models.SizeOption;
using TotalRow = Invoicer.Models.TotalRow;

namespace TCP.Business.Services
{

    public class ExtrasService : ICustomService
    {
        readonly IRepositorySql _repositorySql;
        readonly IRepository<Invoice> _repository;
        public ExtrasService(IRepository<Invoice> repository, IRepositorySql repositorySql)
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
            string spName = KeyBusiness.SP_NAME_LIST;
            DynamicParameters parameters = new();
            parameters.Add(KeyBusiness.SP_PARAM_DATEFROM, datefrom);
            parameters.Add(KeyBusiness.SP_PARAM_DATETO, dateto);

            if (id is not null)
            {
                spName = KeyBusiness.SP_NAME_ALONE;
                parameters.Add(KeyBusiness.SP_PARAM_CLIENTID, id);
            }

            var rawData = _repositorySql.ExecuteStoredProcedure(spName, parameters);

            IEnumerable<InvoiceClientBestSell> spResult = Core.Externals.JsonConvert.DeserializeDynamic<IEnumerable<InvoiceClientBestSell>>(rawData);
            
            string data = $"{KeyBusiness.EXPORT_LOG_SP}: {spName} | {KeyBusiness.EXPORT_LOG_TIME}: {DateTime.Now.ToString()} | {KeyBusiness.EXPORT_LOG_RECORDS}: {spResult.Count()}";
            
            Core.Framework.IO.SaveFile(KeyBusiness.EXPORT_PATH, KeyBusiness.EXPORT_NAME, data);

            return spResult;
        }

        public IEnumerable<InvoiceLineMountTotalsView> ExecuteView(string viewName)
        {
            string query = $"{KeyBusiness.SQL_SELECT} {viewName}";
            var rawData = _repositorySql.ExecuteQuery(query);

            return Core.Externals.JsonConvert.DeserializeDynamic<IEnumerable<InvoiceLineMountTotalsView>>(rawData);
        }

        public void DownloadPdf(int id)
        {
            TCP.Model.Entities.Invoice? invoice = _repository.AsQueryable().Where(x => x.Status == MainStatus.ACTIVE).Include(x => x.Client)
                .Include(x => x.Customer)
                .Include(x => x.Detail).ThenInclude(d => d.Product).FirstOrDefault();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            string path = @"C:\TCP Files\OUT";
            string name = $"TCP_Invoice_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.pdf";
            string fullPath = Path.Combine(Core.Framework.IO.ToFolderNormalize(path), name);

            new InvoicerPdf(SizeOption.A4, OrientationOption.Landscape, "£")
                .TextColor("#CC0000")
                .BackColor("#FFD6CC")
                .Image(@"..\TCP.Docs\tcpmiame.jpg", 250, 50)
                .Company(Address.Make("FROM", new string[] { "Vodafone Limited", "Vodafone House", "The Connection", "Newbury", "Berkshire RG14 2FN" }, "1471587", "569953277"))
                .Client(Address.Make("BILLING TO", new string[] { "Isabella Marsh", "Overton Circle", "Little Welland", "Worcester", "WR## 2DJ" }))
                .Items(new List<ItemRow> {
                            ItemRow.Make("Nexus 6", "Midnight Blue", (decimal)1, 20, (decimal)166.66, (decimal)199.99),
                            ItemRow.Make("24 Months (£22.50pm)", "100 minutes, Unlimited texts, 100 MB data 3G plan with 3GB of UK Wi-Fi", (decimal)1, 20, (decimal)360.00, (decimal)432.00),
                            ItemRow.Make("Special Offer", "Free case (blue)", (decimal)1, 0, (decimal)0, (decimal)0),
                })
                .Totals(new List<TotalRow> {
                            TotalRow.Make("Sub Total", (decimal)526.66),
                            TotalRow.Make("VAT @ 20%", (decimal)105.33),
                            TotalRow.Make("Total", (decimal)631.99, true),
                })
                .Details(new List<DetailRow> {
                            DetailRow.Make("PAYMENT INFORMATION", "Make all cheques payable to Vodafone UK Limited.", "", "If you have any questions concerning this invoice, contact our sales department at sales@vodafone.co.uk.", "", "Thank you for your business.")
                })
                .Footer("http://www.vodafone.co.uk")
                .Save(fullPath);
        }
    }
}