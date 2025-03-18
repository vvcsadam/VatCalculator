using VatCalculator.Api.Application.Interfaces;
using VatCalculatorApi.Application.DTO;
using VatCalculatorApi.Domain.Entities;

namespace VatCalculator.Api.Application.Mappers
{
    public class VatCalculationDtoMapper : IMapper<VatCalculationDto, VatCalculation>
    {
        public void Map(VatCalculationDto sourceEntity, VatCalculation entity)
        {
            if (sourceEntity == null) return;

            entity = entity ?? new VatCalculation();
            entity.Net = sourceEntity.Net ?? 0;
            entity.Gross = sourceEntity.Gross ?? 0;
            entity.Vat = sourceEntity.Vat ?? 0;
            entity.VatRate = sourceEntity.VatRate ?? -1;
        }
    }
}
