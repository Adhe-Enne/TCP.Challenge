using AutoMapper;
using Core.Abstractions;
using TCP.Model.Entities;

namespace TCP.Api.Controllers
{
    public class BonusController : BaseController
    {
        IService<Invoice> _invoiceService;
        IService<ListOption> _listOptionService;
        public BonusController(
            IMapper mapper,
            IService<Invoice> serviceCrud,
            IService<ListOption> listOptionService)
            : base(mapper)
        {
            _invoiceService = serviceCrud;
            _listOptionService = listOptionService;
        }
    }
}
