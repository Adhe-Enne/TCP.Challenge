using Core.Abstractions;
using Core.Framework;
using TCP.BlazorWeb.Constants;
using TCP.BlazorWeb.Interfaces;
using TCP.Model.Dto;

namespace TCP.BlazorWeb.Services
{
    public class InvoiceService : IInvoiceService
    {
        public InvoiceService()
        {

        }

        public Task<IGridResult<TCP.Model.Dto.ClientDto>> GetClients()
        {
            IGridResult<TCP.Model.Dto.ClientDto> response = new GridResult<ClientDto>();

            try
            {
                response = Core.Externals.RestCall.Get<GridResult<TCP.Model.Dto.ClientDto>>(RestConstants.API_URL, RestConstants.CLIENT);
            }
            catch (Exception ex)
            {
                response.Set(ex.Message, true);
            }

            return Task.FromResult(response);
        }

        public Task<IGenericResult> Save(InvoiceCreateDto entity)
        {
            IGenericResult response = new GenericResult();

            try
            {
                response = Core.Externals.RestCall.Post<GenericResult>(RestConstants.API_URL, RestConstants.INVOICE, entity);
            }
            catch (Exception ex)
            {
                response.Set(ex.Message, true);
            }

            return Task.FromResult(response);
        }

        public Task<IGenericResult> Update(InvoiceUpdateDto entity)
        {
            IGenericResult response = new GenericResult();

            try
            {
                response = Core.Externals.RestCall.Put<GenericResult>(RestConstants.API_URL, RestConstants.INVOICE, entity);
            }
            catch (Exception ex)
            {
                response.Set(ex.Message, true);
            }

            return Task.FromResult(response);
        }

        public Task<IGridResult<TCP.Model.Dto.ListOptionDto>> GetStatus()
        {
            IGridResult<TCP.Model.Dto.ListOptionDto> response = new GridResult<ListOptionDto>();

            try
            {
                response = Core.Externals.RestCall.Get< GridResult<ListOptionDto>>(RestConstants.API_URL, $"{RestConstants.EXTRAS}/{RestConstants.LIST_OPT}/{RestConstants.INV_OPT}");
            }
            catch (Exception ex)
            {
                response.Set(ex.Message, true);
            }

            return Task.FromResult(response);
        }

        public Task<IGridResult<TCP.Model.Dto.ListOptionDto>> GetPayMethods()
        {
            IGridResult<TCP.Model.Dto.ListOptionDto> response = new GridResult<ListOptionDto>();

            try
            {
                response = Core.Externals.RestCall.Get<GridResult<ListOptionDto>>(RestConstants.API_URL, $"{RestConstants.EXTRAS}/{RestConstants.LIST_OPT}/{RestConstants.PAY_OPT}");
            }
            catch (Exception ex)
            {
                response.Set(ex.Message, true);
            }

            return Task.FromResult(response);
        }

        public Task<IGridResult<TCP.Model.Dto.CustomerDto>> GetCustomers()
        {
            IGridResult<TCP.Model.Dto.CustomerDto> response = new GridResult<CustomerDto>();

            try
            {
                response = Core.Externals.RestCall.Get<GridResult<CustomerDto>>(RestConstants.API_URL, $"{RestConstants.EXTRAS}/{RestConstants.CUSTOMER}");
            }
            catch (Exception ex)
            {
                response.Set(ex.Message, true);
            }

            return Task.FromResult(response);
        }
    }
}
