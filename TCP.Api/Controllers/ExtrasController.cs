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
    /// <summary>
    /// Se encarga de Get's breves y puntuales.
    /// Para evitar un controller de cada envidad sin necesidad de logica alguna.
    /// </summary>
    [Route("api/[controller]")]
    public class ExtrasController : BaseController
    {
        readonly IService<Invoice> _invoiceService;
        readonly IService<Customer> _customerService;
        readonly IService<ListOption> _listOptionService;
        readonly ICustomService _service;
        public ExtrasController(
            IMapper mapper,
            IService<Invoice> serviceCrud,
            IService<ListOption> listOptionService,
            ICustomService queryFactory,
            IService<Customer> icustomerService)
            : base(mapper)
        {
            _invoiceService = serviceCrud;
            _listOptionService = listOptionService;
            _service = queryFactory;
            _customerService = icustomerService;
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

        [HttpGet("pdf/{id}")]

        public IGridResult<InvoiceLineMountTotalsView> GetDbView(int id)
        {
            IGridResult<InvoiceLineMountTotalsView> response = new GridResult<InvoiceLineMountTotalsView>();

            try
            {
                _service.DownloadPdf(id);

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

        [HttpGet("listoption/{option}")]

        public IGridResult<ListOptionDto> GetListOption(string option)
        {
            IGridResult<ListOptionDto> response = new GridResult<ListOptionDto>();

            try
            {
                IEnumerable<ListOption> options = _listOptionService.AsQueryable().Where(x => x.Status == Model.Enums.MainStatus.ACTIVE && x.OptionType == option).ToList();
                response.Data = _mapper.Map<IEnumerable<ListOptionDto>>(options);

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


        [HttpGet("customer")]

        public IGridResult<CustomerDto> GetCustomers()
        {
            IGridResult<CustomerDto> response = new GridResult<CustomerDto>();

            try
            {
                IEnumerable<Customer> options = _customerService.AsQueryable().Where(x => x.Status == Model.Enums.MainStatus.ACTIVE).ToList();
                response.Data = _mapper.Map<IEnumerable<CustomerDto>>(options);

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