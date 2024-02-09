using Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Model.Entities
{
    public class Product : Core.Abstractions.IEntity, IDatetimeManaged
    {
        public int Id {get;set;}
        public MainStatus Status {get;set;}
        public decimal Price {get;set;}
        public string? Code {get;set;}
        public string? Description { get; set; }
        public DateTime? DateAdded {get;set;}
        public DateTime? DateUpdated {get;set;}

    }
}
