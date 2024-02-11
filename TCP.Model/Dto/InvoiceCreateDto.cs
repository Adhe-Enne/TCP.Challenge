namespace TCP.Model.Dto
{
    public class InvoiceCreateDto
    {
        public int ClientId { get; set; }
        public int CustomerId { get; set; }
        public int PaymentMethod { get; set; }
        public ICollection<InvoiceDetailCreateDto> Detail { get; set; }
    }
}
