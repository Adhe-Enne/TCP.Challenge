using System;

namespace Core.Abstractions
{
    public interface IDatetimeManaged
    {
        DateTime? DateUpdated { get; set; }
        DateTime? DateAdded { get; set; }
    }
}
