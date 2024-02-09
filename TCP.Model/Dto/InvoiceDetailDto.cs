namespace TCP.Model.Dto
{
    public class InvoiceDetailDto
    {
        public int Id { get; set; }
        public int FactId { get; set; }
        public DateTime? DateAdded { get; set; }
        public int ProductId { get; set; }
        public string? Product { get; set; }
        public string? ProductCode { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }
        public decimal LineAmount { get; set; }
    }
}
