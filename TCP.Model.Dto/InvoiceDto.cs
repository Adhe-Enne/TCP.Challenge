namespace TCP.Model.Dto
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CustomerId { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DueDate { get; set; }
        public int? StatusCode { get; set; }
        public int? PaymentCode { get; set; }
        public string InvoiceStatus { get; set; }
        public string StatusDescription { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhone { get; set; }
        public string ClientAdress { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyAdress { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentDescription { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal TotalQty { get; set; }
        public ICollection<InvoiceDetailDto> Detail { get; set; }
    }
}
