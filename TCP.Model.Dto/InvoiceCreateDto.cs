namespace TCP.Model.Dto
{
    public class InvoiceCreateDto
    {
        public string ClientId { get; set; }
        public string CustomerId { get; set; }
        public string PaymentMethod { get; set; }
        public ICollection<InvoiceDetailCreateDto> Detail { get; set; }
    }
}
