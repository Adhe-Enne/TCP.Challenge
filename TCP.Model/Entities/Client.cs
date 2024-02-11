using Core.Abstractions;
using TCP.Model.Enums;
using TCP.Model.Interfaces;

namespace TCP.Model.Entities
{
    public class Client : Core.Abstractions.IEntity, IDatetimeManaged, IBusinessEntity
    {
        public Client()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? CUIT { get; set; }
        public string? Adress { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool Disabled { get; set; }
        public MainStatus Status { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateAdded { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
