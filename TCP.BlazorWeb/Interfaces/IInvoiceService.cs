using Core.Abstractions;
using TCP.Model.Dto;

namespace TCP.BlazorWeb.Interfaces
{
    public interface IInvoiceService
    {
        Task<IGridResult<TCP.Model.Dto.ClientDto>> GetClients();
        Task<IGenericResult> Save(InvoiceCreateDto entity);
        Task<IGenericResult> Update(InvoiceUpdateDto entity);
        Task<IGridResult<TCP.Model.Dto.ListOptionDto>> GetPayMethods();
        Task<IGridResult<TCP.Model.Dto.ListOptionDto>> GetStatus();
        Task<IGridResult<TCP.Model.Dto.CustomerDto>> GetCustomers();
    }
}
