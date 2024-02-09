using Core.Abstractions;

namespace TCP.Model.Entities
{
    public class Client : Core.Abstractions.IEntity, IDatetimeManaged
    {
        public Client()
        {
            
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string? CUIT { get; set; }
        public string? Adress { get; set; }
        public bool Disabled { get; set; }
        public MainStatus Status { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
