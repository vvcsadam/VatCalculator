using VatCalculatorApi.Application.Interfaces;
using VatCalculatorApi.Application.Validators;
using VatCalculatorApi.Domain.Entities;

namespace VatCalculatorApi.Application.Services
{
    public class VatCalculatorService : IVatCalculatorService
    {
        public VatCalculation CalculateVat(decimal? net, decimal? gross, decimal? vat, int? vatRate)
        {
            var vatCalculation = new VatCalculation
            {
                Net = net ?? 0,
                Gross = gross ?? 0,
                Vat = vat ?? 0,
                VatRate = vatRate ?? 0
            };

            VatCalculationValidator.Validate(vatCalculation);

            var calculatedNet = net > 0 ? net.Value : (gross > 0 ? gross.Value / (1 + (decimal)vatRate.Value / 100) : (vat.Value / ((decimal)vatRate.Value / 100)));
            var calculatedGross = gross > 0 ? gross.Value : (net > 0 ? net.Value * (1 + (decimal)vatRate.Value / 100) : vat.Value + calculatedNet);
            var calculatedVat = vat > 0 ? vat.Value : (gross > 0 ? gross.Value - calculatedNet : calculatedNet * (decimal)vatRate.Value / 100);

            return new VatCalculation
            {
                Net = calculatedNet,
                Gross = calculatedGross,
                Vat = calculatedVat,
                VatRate = vatRate.Value
            };
        }
    }
}
