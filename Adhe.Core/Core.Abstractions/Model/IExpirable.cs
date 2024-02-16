namespace Core.Abstractions.Model
{
    public interface IExpirable
    {
        DateTime? DateAdded { get; set; }
        DateTime? DueDate { get; set; }
    }
}
