

namespace TCP.Model.ViewSp
{
    public class InvoiceClientBestSell
    {
        public int Factura { get; set; }
        public int Cliente { get; set; }
        public int Vendedor { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Qty { get; set; }
        public int ProductId { get; set; }
        public string TopSellingProduct { get; set; }
    }
}
