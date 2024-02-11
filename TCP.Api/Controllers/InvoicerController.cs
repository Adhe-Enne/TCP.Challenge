using AutoMapper;
using Azure.Core;
using Core.Abstractions;
using Core.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TCP.Api.Profiles;
using TCP.Model.Constants;
using TCP.Model.Dto;
using TCP.Model.Entities;
using TCP.Model.Enums;
using TCP.Model.Request;

namespace TCP.Api.Controllers
{
    [Route("/api/[controller]")]
    public class InvoicerController : BaseController
    {
        IService<Invoice> _invoiceService;
        IService<InvoiceDetail> _invoiceDetailService;
        IService<ListOption> _listOptionService;
        public InvoicerController(
            IMapper mapper,
            IService<Invoice> serviceCrud,
            IService<ListOption> listOptionService,
            IService<InvoiceDetail> invoiceDetailService)
            : base(mapper)
        {
            _invoiceService = serviceCrud;
            _listOptionService = listOptionService;
            _invoiceDetailService = invoiceDetailService;
        }

        /// <summary>
        /// Endpoint excesivamente largo producto de un dto con valores de diferentes tablas.
        /// Se plante una capa intermedia "ViewLayerMapper" pero se descarto ya que solamente este endpoint tuvo complejidad de mapeo, 
        /// misma que yo agregue con animos de ampliar la logica.
        /// Edit: Se desplazo logica de map en MapHelper. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IGridResult<InvoiceDto> GetAll()
        {
            IGridResult<InvoiceDto> response = new GridResult<InvoiceDto>();

            try
            {
                IEnumerable<Invoice> entities = _invoiceService.AsQueryable();

                if (!entities.Any())
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                    return response;
                }

                IQueryable<ListOption> listOptions = _listOptionService.AsQueryable().Where(x => x.Status == Model.Enums.MainStatus.ACTIVE);
                response.Data = MapHelper.MapInvoiceDto(entities, listOptions, _mapper);

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

        private IGridResult<InvoiceDto> GetAll(InvoiceRequest request)
        {
            IGridResult<InvoiceDto> response = new GridResult<InvoiceDto>();

            try
            {
                IEnumerable<Invoice> entities = _invoiceService.AsQueryable();

                if (!entities.Any())
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                    return response;
                }

                IQueryable<ListOption> listOptions = _listOptionService.AsQueryable().Where(x => x.Status == Model.Enums.MainStatus.ACTIVE);
                response.Data = MapHelper.MapInvoiceDto(entities, listOptions, _mapper);

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

        /// <summary>
        /// A fin de optimizar el front, dejamos la opcion de retornar solo la cabecera de las facturas
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpGet("head/{id}")]

        public IGridResult<InvoiceHeadDto> GetHead(int id)
        {
            IGridResult<InvoiceHeadDto> response = new GridResult<InvoiceHeadDto>();

            try
            {
                IEnumerable<Invoice> src = _invoiceService.Filter(x => x.Id == id && x.Status == MainStatus.ACTIVE).AsQueryable();

                response.Data = _mapper.Map<IEnumerable<InvoiceHeadDto>>(src);

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
        /// <summary>
        /// A fin de optimizar el front, dejamos la posibilidad de retornar solo el detalle de una factura
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        [HttpGet("detail/{invoiceId}")]

        public IGridResult<InvoiceDetailDto> GetDetail(int invoiceId)
        {
            IGridResult<InvoiceDetailDto> response = new GridResult<InvoiceDetailDto>();

            try
            {
                IEnumerable<InvoiceDetail> src = _invoiceDetailService.AsQueryable().Where(x =>x.Id == invoiceId && x.Status == MainStatus.ACTIVE);
                response.Data = _mapper.Map<IEnumerable<InvoiceDetailDto>>(src);

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

        [HttpPost]
        public IGenericResult Insert([FromBody] InvoiceCreateDto entity)
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

        /// <summary>
        /// Aunque no actualiza todos los campos, ciertamente revalida y recalcula toda la factura.
        /// Por eso consideramos mejor PUT
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        public IGenericResult Update([FromBody] InvoiceUpdateDto entity)
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
