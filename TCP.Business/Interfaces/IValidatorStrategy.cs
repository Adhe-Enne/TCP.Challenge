namespace TCP.Business.Interfaces
{
    public interface IValidatorStrategy<T>
    {
        void ValidateFields(T entity);
    }
}