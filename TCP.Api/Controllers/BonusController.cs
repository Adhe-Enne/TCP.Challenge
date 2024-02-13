using AutoMapper;
using Core.Abstractions;
using Core.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.InteropServices;
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
        readonly ICustomService _service;
        public BonusController(
            IMapper mapper,
            IService<Invoice> serviceCrud,
            IService<ListOption> listOptionService,
            ICustomService queryFactory)
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

        [HttpGet("{datefrom}/{dateto}/{id?}/")]
        public IGridResult<InvoiceClientBestSell> GetInvoiceDbView(string datefrom, string dateto, int? id = null)
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

        [HttpGet("view/{viewname}")]

        public IGridResult<InvoiceLineMountTotalsView> GetDbView(string viewname)
        {
            IGridResult<InvoiceLineMountTotalsView> response = new GridResult<InvoiceLineMountTotalsView>();

            try
            {
                response.Data = _service.ExecuteView(viewname);

                if (response.TotalRecords == 0)
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                response.Set(HandleException(Model.Constants.Messages.SP_INVALID, ex));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            catch (Exception ex)
            {
                response.Set(HandleException(ex));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return response;
        }

        [HttpPost("pdf/{id}")]

        public IGridResult<InvoiceLineMountTotalsView> GetDbView(int id)
        {
            IGridResult<InvoiceLineMountTotalsView> response = new GridResult<InvoiceLineMountTotalsView>();

            try
            {
                 _service.DownloadPdf();

                if (response.TotalRecords == 0)
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                response.Set(HandleException(Model.Constants.Messages.SP_INVALID, ex));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
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