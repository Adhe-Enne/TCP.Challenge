using Core.Abstractions;
using Core.Framework;
using TCP.BlazorWeb.Constants;
using TCP.BlazorWeb.Interfaces;
using TCP.Model.Dto;
namespace TCP.BlazorWeb.Services
{
    public class ClientService : IClientService
    {
        public ClientService()
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

        public Task<IGenericResult> Save(ClientCreateDto entity)
        {
            IGenericResult response = new GenericResult();

            try
            {
                response = Core.Externals.RestCall.Post<GenericResult>(RestConstants.API_URL, RestConstants.CLIENT, entity);
            }
            catch (Exception ex)
            {
                response.Set(ex.Message, true);
            }

            return Task.FromResult(response);
        }

        public Task<IGenericResult> Update(ClientDto entity)
        {
            IGenericResult response = new GenericResult();

            try
            {
                response = Core.Externals.RestCall.Put<GenericResult>(RestConstants.API_URL, RestConstants.CLIENT, entity);
            }
            catch (Exception ex)
            {
                response.Set(ex.Message, true);
            }

            return Task.FromResult(response);
        }
    }
}
