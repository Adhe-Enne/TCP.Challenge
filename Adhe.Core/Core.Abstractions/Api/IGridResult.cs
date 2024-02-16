using System.Collections.Generic;

namespace Core.Abstractions
{
    public interface IGridResult<T> : IGenericResult
    {
        IEnumerable<T> Data { get; set; }

        int TotalRecords { get; set; }
    }
}
