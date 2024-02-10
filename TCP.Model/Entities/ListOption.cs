using Core.Abstractions;
using TCP.Model.Enums;
using TCP.Model.Interfaces;

namespace TCP.Model.Entities
{
    public class ListOption : Core.Abstractions.IEntity, IDatetimeManaged, IBusinessEntity
    {
        public int Id { get; set; }
        public string? OptionType { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public MainStatus Status{ get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
