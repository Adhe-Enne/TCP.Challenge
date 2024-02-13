using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Model.ViewSp
{
    public class InvoiceLineMountTotalsView
    {
        public int Factura { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public decimal MontoTotal { get; set; }
    }
}
