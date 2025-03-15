using VatCalculatorApi.Domain.Entities;
using VatCalculatorApi.Domain.Enums;

namespace VatCalculatorApi.Application.Interfaces
{
    public interface IVatCalculatorService
    {
        VatCalculation CalculateVat(decimal? vat, decimal? net, decimal? gross, int? vatRate);
    }
}
