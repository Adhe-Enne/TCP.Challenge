using TCP.Model.Enums;

namespace TCP.Model.Interfaces
{
    public interface IBusinessEntity
    {
        MainStatus Status { get; set; }
    }
}
