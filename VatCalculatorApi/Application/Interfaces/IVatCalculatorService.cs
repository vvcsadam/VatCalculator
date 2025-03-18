using VatCalculatorApi.Application.DTO;
using VatCalculatorApi.Domain.Entities;
using VatCalculatorApi.Domain.Enums;

namespace VatCalculatorApi.Application.Interfaces
{
    public interface IVatCalculatorService
    {
        VatCalculation CalculateVat(VatCalculationDto vatCalculationDto);
    }
}
