namespace VatCalculator.Api.Application.Interfaces
{
    public interface IMapper<TSource, TDestination>
    {
        void Map(TSource sourceEntity, TDestination entity);
    }
}
