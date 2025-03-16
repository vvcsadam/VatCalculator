using VatCalculatorApi.Domain.Entities;
using VatCalculatorApi.Domain.Enums;

namespace VatCalculatorApi.Application.Interfaces
{
    public interface IVatCalculatorService
    {
        VatCalculation CalculateVat(decimal? net, decimal? gross, decimal? vat, int? vatRate);
    }
}
