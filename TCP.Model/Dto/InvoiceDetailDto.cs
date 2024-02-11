namespace TCP.Model.Dto
{
    public class InvoiceDetailDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public DateTime? DateAdded { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductCode { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineAmount { get; set; }
    }
}
