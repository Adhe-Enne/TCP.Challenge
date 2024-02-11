namespace TCP.Model.Dto
{
    public class InvoiceUpdateDto
    {
        public int? Id { get; set; }
        public int? ClientId { get; set; }
        public int? CustomerId { get; set; }
        public int? PaymentMethod { get; set; }
        public ICollection<InvoiceDetailUpdateDto> Detail { get; set; }
    }
}
