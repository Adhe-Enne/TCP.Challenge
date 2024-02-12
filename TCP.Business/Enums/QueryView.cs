using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Business.Enums
{
    public static class QueryConstants
    {
        public const string FIRST_VIEW = @"
                                            SELECT I.Id [Invoice], I.DateAdded,LP.[Name] [Estado], LP.[Description] [Descripcion], SUM(D.LineAmount) MontoTotal
                                            FROM Invoice I
                                            INNER JOIN Invoice_Detail D on I.Id = D.InvoiceId
                                            INNER JOIN ListOption LP ON LP.OptionId = I.InvoiceStatus
                                            GROUP BY I.Id, I.DateAdded,LP.[Name], LP.[Description]
                                                                ";        
        
        public const string SECOND_VIEW = @"
                                            SELECT I.Id [Factura], I.DateAdded [Fecha Alta], C.CompanyName [RazonSocial], C.CUIT
                                            FROM Invoice I
                                            INNER JOIN Client C on C.Id = I.ClientId
                                                                ";        
    }
}
