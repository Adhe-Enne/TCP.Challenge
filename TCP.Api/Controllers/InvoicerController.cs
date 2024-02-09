using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace TCP.Api.Controllers
{
    [Route("/api/[controller]")]
    public class InvoicerController : BaseController
    {
        public InvoicerController(IMapper mapper) : base(mapper) 
        { 
        }
    }
}
