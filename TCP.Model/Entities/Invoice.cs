using Core.Abstractions;
using TCP.Model.Enums;

namespace TCP.Model.Entities
{
    public class Invoice : Core.Abstractions.IEntity, IDatetimeManaged
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string? Description { get; set; }
        public decimal TotalAmount { get; set; }
        public MainStatus Status { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }

        public ICollection<InvoiceDetail> Detail { get; set; }
    }
}
