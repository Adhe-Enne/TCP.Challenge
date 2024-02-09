using Core.Abstractions;

namespace TCP.Model.Entities
{
    public class InvoiceDetail : Core.Abstractions.IEntity, IDatetimeManaged
    {
        public InvoiceDetail()
        {
                
        }

        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int ProductId { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }
        public decimal LineAmount { get; set; }
        public MainStatus Status { get; set; }
        public Invoice? Invoice { get; set; }
        public Product? Product { get; set; }
    }
}
