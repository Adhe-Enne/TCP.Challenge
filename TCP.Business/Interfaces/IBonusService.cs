using Microsoft.Data.SqlClient;
using TCP.Model.Entities;
using TCP.Model.ViewSp;

namespace TCP.Business.Interfaces
{
    /// <summary>
    /// Unico Servicio Custom, la generalidad no aplica en este servicio
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBonusService
    {
        IQueryable<Invoice> GetQuery(Business.Enums.QueryType queryType);

        IEnumerable<InvoiceClientBestSell> GetSp(string datefrom, string dateto, int? id);
    }
}
