using TCP.Model.Entities;
using TCP.Model.ViewSp;

namespace TCP.Business.Interfaces
{
    /// <summary>
    /// Unico Servicio Custom, la generalidad no aplica en lo absoluto.
    /// Inicialmente se mantuvo la parametrizacion y dinamismo como en otros servicios, 
    /// pero para el proposito de sus funciones, el esfuerzo se elevo enormemente y se vio innecesario hacerlo configurable, 
    /// Se resuelve la logica de manera puntual (ej exportacion de sp) mediante constantes.
    /// </summary>
    public interface ICustomService
    {
        IQueryable<Invoice> GetQuery(Business.Enums.QueryType queryType);
        IEnumerable<InvoiceClientBestSell> GetSp(string datefrom, string dateto, int? id);
        IEnumerable<InvoiceLineMountTotalsView> ExecuteView(string viewName);
        void DownloadPdf(int id);
    }
}
