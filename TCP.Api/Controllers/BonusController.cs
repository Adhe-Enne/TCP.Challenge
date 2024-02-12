using AutoMapper;
using Core.Abstractions;
using Core.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TCP.Business.Enums;
using TCP.Business.Interfaces;
using TCP.Model.Dto;
using TCP.Model.Entities;
using TCP.Model.ViewSp;

namespace TCP.Api.Controllers
{
    [Route("api/[controller]")]
    public class BonusController : BaseController
    {
        readonly IService<Invoice> _invoiceService;
        readonly IService<ListOption> _listOptionService;
        readonly IBonusService _service;
        public BonusController(
            IMapper mapper,
            IService<Invoice> serviceCrud,
            IService<ListOption> listOptionService,
            IBonusService queryFactory)
            : base(mapper)
        {
            _invoiceService = serviceCrud;
            _listOptionService = listOptionService;
            _service = queryFactory;
        }


        [HttpGet("query/{queryType}")]
        public IGridResult<InvoiceDto> GetQuery(QueryType queryType) 
        {
            IGridResult<InvoiceDto> response = new GridResult<InvoiceDto>();

            try
            {
                IQueryable<Invoice> src = _service.GetQuery(queryType);
                response.Data = _mapper.Map<IEnumerable<InvoiceDto>>(src);

                if (response.TotalRecords == 0)
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Set(HandleException(ex));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return response;
        }

        [HttpGet("sp/{datefrom}/{dateto}/{id}")]
        public IGridResult<InvoiceClientBestSell> GetOneInvoiceDbView(string datefrom, string dateto, int id)
        {
            return GetDbSp(datefrom, dateto, id);
        }

        [HttpGet("sp/{datefrom}/{dateto}")]
        public IGridResult<InvoiceClientBestSell> GetAllInvoiceDbView(string datefrom, string dateto)
        {
            return GetDbSp(datefrom, dateto);
        }

        private IGridResult<InvoiceClientBestSell> GetDbSp(string datefrom, string dateto, int? id = null)
        {
            IGridResult<InvoiceClientBestSell> response = new GridResult<InvoiceClientBestSell>();

            try
            {
                var src = _service.GetSp(datefrom, dateto, id);
                response.Data = _mapper.Map<IEnumerable<InvoiceClientBestSell>>(src);

                if (response.TotalRecords == 0)
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Set(HandleException(ex));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return response;
        }

        [HttpGet("view")]

        public IGridResult<InvoiceClientBestSell> GetDbView()
        {
            IGridResult<InvoiceClientBestSell> response = new GridResult<InvoiceClientBestSell>();

            try
            {
             //   var src = _service.GetSp();
             //   response.Data = _mapper.Map<IEnumerable<InvoiceClientBestSell>>(src);

                if (response.TotalRecords == 0)
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Set(HandleException(ex));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}