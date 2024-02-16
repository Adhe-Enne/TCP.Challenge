using Core.Abstractions;
using TCP.Model.Dto;

namespace TCP.BlazorWeb.Services
{
    public interface IClientService
    {
        Task<IGridResult<TCP.Model.Dto.ClientDto>> GetClients();
        Task<IGenericResult> Save(ClientCreateDto entity);
        Task<IGenericResult> Update(ClientDto entity);
    }
}
