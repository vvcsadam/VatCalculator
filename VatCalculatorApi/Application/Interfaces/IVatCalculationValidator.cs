namespace VatCalculatorApi.Application.Interfaces
{
    public interface IValidator<T>
    {
        void Validate(T entity);
    }
}
