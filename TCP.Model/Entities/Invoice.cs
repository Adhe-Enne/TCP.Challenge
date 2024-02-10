using Core.Abstractions;
using TCP.Model.Enums;
using TCP.Model.Interfaces;

namespace TCP.Model.Entities
{
    public class Invoice : Core.Abstractions.IEntity, IDatetimeManaged , IBusinessEntity
    {
        public Invoice()
        {
            Detail = new HashSet<InvoiceDetail>();    
        }

        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalQty{ get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public MainStatus Status { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DueDate { get; set; }
        public Client? Client { get; set; }
        public ICollection<InvoiceDetail> Detail { get; set; }
        public virtual Customer Customer { get; set; }

    }
}
