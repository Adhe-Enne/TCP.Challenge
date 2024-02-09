using Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Model.Entities
{
    public class ListOption : Core.Abstractions.IEntity, IDatetimeManaged
    {
        public int Id { get; set; }

        public string? OptionType { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public DateTime? DateUpdated { get; set; }
        public DateTime? DateAdded { get ; set; }
    }
}
