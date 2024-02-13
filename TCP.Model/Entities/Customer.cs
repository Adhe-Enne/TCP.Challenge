using Core.Abstractions;
using TCP.Model.Enums;
using TCP.Model.Interfaces;

namespace TCP.Model.Entities
{
    public class Customer : IEntity , IBusinessEntity
    {
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
            Status = MainStatus.ACTIVE;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public MainStatus Status { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        
        public ICollection<Invoice> Invoices { get; set; }

    }
}
