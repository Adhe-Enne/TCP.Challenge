using AutoMapper;
using Core.Abstractions;
using Core.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TCP.Model.Dto;
using TCP.Model.Entities;
using TCP.Model.Enums;
using TCP.Model.Request;

namespace TCP.Api.Controllers
{
    [Route("/api/[controller]")]
    public class InvoicerController : BaseController
    {
        IServiceCrud<Invoice> _invoiceService;
        public InvoicerController(IMapper mapper, IServiceCrud<Invoice> serviceCrud) : base(mapper)
        {
            _invoiceService = serviceCrud;
        }

        [HttpGet]
        public IGridResult<InvoiceDto> GetAll()
        {
            IGridResult<InvoiceDto> response = new GridResult<InvoiceDto>();

            try
            {
                IEnumerable<Invoice> src = _invoiceService.Filter(x=> x.Status == MainStatus.ACTIVE);
                response.Data = _mapper.Map<IEnumerable<InvoiceDto>>(src);
            }
            catch (Exception ex)
            {
                response.Set(HandleException(ex));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                response.TotalRecords = response.Data.Count();

                if (response.TotalRecords == 0)
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }

            return response;
        }

        [HttpGet("InvoiceCode/{InvoiceCode}")]
        public IGridResult<InvoiceDto> GetByCompany(int invoicecode)
        {
            InvoiceRequest InvoiceDto = new InvoiceRequest();
            InvoiceDto.InvoiceCode = invoicecode;

            return GetAll(InvoiceDto);
        }

        [HttpGet("{clientid}")]

        public IGridResult<InvoiceDto> GetByCuit(int clientid)
        {
            InvoiceRequest InvoiceDto = new InvoiceRequest();
            InvoiceDto.ClientId = clientid;

            return GetAll(InvoiceDto);
        }

        [HttpGet("byrequest")]

        public IGridResult<InvoiceDto> GetAll(InvoiceRequest request)
        {
            IGridResult<InvoiceDto> response = new GridResult<InvoiceDto>();

            try
            {
                IEnumerable<Invoice> src = _invoiceService
                    .Filter(x => (x.ClientId == request.ClientId || x.Id == request.InvoiceCode) && x.Status < MainStatus.ACTIVE
                    );

                response.Data = _mapper.Map<IEnumerable<InvoiceDto>>(src);
            }
            catch (Exception ex)
            {
                response.Set(HandleException(ex));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            finally
            {
                response.TotalRecords = response.Data.Count();

                if (response.TotalRecords == 0)
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }

            return response;
        }

        [HttpPost]
        public IGenericResult Insert([FromBody] InvoiceDto entity)
        {
            LogInfo(Model.Constants.Messages.ENTITY_INSERT, entity);
            IGenericResult result = new GenericResult();

            try
            {
                Invoice Invoice = _mapper.Map<Invoice>(entity);
                result = _invoiceService.Insert(Invoice);
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return result;
        }

        [HttpPatch]
        public IGenericResult Update([FromBody] InvoiceDto entity)
        {
            LogInfo(Model.Constants.Messages.ENTITY_UPDATE, entity);
            IGenericResult result = new GenericResult();

            try
            {
                Invoice Invoice = _mapper.Map<Invoice>(entity);
                result = _invoiceService.Update(Invoice);
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return result;
        }

        [HttpDelete("{id}")]
        public IGenericResult LogicDelete(int id)
        {
            LogInfo(Model.Constants.Messages.ENTITY_DELETE);
            IGenericResult result = new GenericResult();

            try
            {
                result = _invoiceService.LogicDelete(id);
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return result;
        }

        [HttpDelete("permanently/{id}")]
        public IGenericResult PhysicDelete(int id)
        {
            LogInfo(Model.Constants.Messages.ENTITY_DELETE_PERMANETLY);
            IGenericResult result = new GenericResult();

            try
            {
                result = _invoiceService.PhysicDelete(id);
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return result;
        }
    }
}
