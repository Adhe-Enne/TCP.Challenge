using AutoMapper;
using Core.Abstractions;
using Core.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TCP.Api.Profiles;
using TCP.Business.Services;
using TCP.Model.Dto;
using TCP.Model.Entities;
using TCP.Repository;

namespace TCP.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        IService<Product> _service;
        public ProductController(IMapper mapper, IService<Product> service) : base(mapper)
        {
            _service = service;
        }

        [HttpGet]
        public IGridResult<ProductDto> GetAll()
        {
            IGridResult<ProductDto> response = new GridResult<ProductDto>();

            try
            {
                IEnumerable<Product> src = _service.GetAll().ToList();
                response.Data = _mapper.Map<IEnumerable<ProductDto>>(src);

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
