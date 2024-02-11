using AutoMapper;
using Core.Abstractions;
using Core.Framework;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TCP.Model.Dto;
using TCP.Model.Entities;
using TCP.Model.Request;

namespace TCP.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : BaseController
    {
        IService<Client> _clientService;

        public ClientController(
            IMapper mapper,
            IService<Client> serviceCrud
            ) : base(
                mapper
                )
        {
            _clientService = serviceCrud;
        }

        [HttpGet]
        public IGridResult<ClientDto> GetAll()
        {
            IGridResult<ClientDto> response = new GridResult<ClientDto>();

            try
            {
                IEnumerable<Client> src = _clientService.Filter(x => x.Disabled == false && x.Status == Model.Enums.MainStatus.ACTIVE).ToList();
                response.Data = _mapper.Map<IEnumerable<ClientDto>>(src);

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

        [HttpGet("company/{company}")]
        public IGridResult<ClientDto> GetByCompany(string company)
        {
            ClientRequest clientDto = new ClientRequest();
            clientDto.Company = company;
            return GetAllByRequest(clientDto);
        }

        [HttpGet("cuit/{cuit}")]

        public IGridResult<ClientDto> GetByCuit(string cuit)
        {
            ClientRequest clientDto = new ClientRequest();
            clientDto.Cuit = cuit;

            return GetAllByRequest(clientDto);
        }

        private IGridResult<ClientDto> GetAllByRequest(ClientRequest request)
        {
            IGridResult<ClientDto> response = new GridResult<ClientDto>();

            try
            {
                IEnumerable<Client> src = _clientService.Filter(x => 
                (x.CompanyName == request.Company || x.CUIT == request.Cuit) 
                && x.Disabled == false && x.Status == Model.Enums.MainStatus.ACTIVE
                );

                response.Data = _mapper.Map<IEnumerable<ClientDto>>(src);

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
        public IGenericResult Insert([FromBody] ClientCreateDto entity)
        {
            LogInfo(Model.Constants.Messages.ENTITY_INSERT, entity);
            IGenericResult result = new GenericResult();

            try
            {
                Client client = _mapper.Map<Client>(entity);
                result = _clientService.Insert(client);
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex));
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            return result;
        }

        [HttpPatch]
        public IGenericResult Update([FromBody] ClientDto entity)
        {
            LogInfo(Model.Constants.Messages.ENTITY_UPDATE, entity);
            IGenericResult result = new GenericResult();

            try
            {
                Client client = _mapper.Map<Client>(entity);
                result = _clientService.Update(client);
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
                result = _clientService.LogicDelete(id);
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
                result = _clientService.PhysicDelete(id);
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
