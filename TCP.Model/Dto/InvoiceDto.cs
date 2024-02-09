namespace TCP.Model.Dto
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime? DateAdded { get; set; }
        public int? StatusCode { get; set; }
        public string InvoiceStatus { get; set; }
        public string StatusDescription { get; set; }
        public ICollection<InvoiceDetailDto> Detail { get; set; }
    }
}
