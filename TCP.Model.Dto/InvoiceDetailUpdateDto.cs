using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Model.Dto
{
    public class InvoiceDetailUpdateDto
    {
        public int? Id { get; set; }
        public int? InvoiceId { get; set; }  
        public int? ProductId { get; set; }
        public decimal? Qty { get; set; }
    }
}
