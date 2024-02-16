using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Model.Dto
{
    public class ClientCreateDto
    {
        public string? CompanyName { get; set; }
        public string? CUIT { get; set; }
        public string? Adress { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
