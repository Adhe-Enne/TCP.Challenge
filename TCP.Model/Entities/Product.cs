using Core.Abstractions;
using TCP.Model.Enums;
using TCP.Model.Interfaces;

namespace TCP.Model.Entities
{
    public class Product : Core.Abstractions.IEntity, IDatetimeManaged, IBusinessEntity
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public MainStatus Status { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
    }
}
