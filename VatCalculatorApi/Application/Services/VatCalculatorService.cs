using VatCalculatorApi.Application.Interfaces;
using VatCalculatorApi.Application.Validators;
using VatCalculatorApi.Domain.Entities;
using VatCalculatorApi.Domain.Enums;

namespace VatCalculatorApi.Application.Services
{
    public class VatCalculatorService : IVatCalculatorService
    {
        public VatCalculation CalculateVat(decimal? vat, decimal? net, decimal? gross, VatRate vatRate)
        {
            var vatCalculation = new VatCalculation
            {
                Net = net ?? 0,
                Gross = gross ?? 0,
                Vat = vat ?? 0,
                VatRate = vatRate
            };

            VatCalculationValidator.Validate(vatCalculation);

            int vatRateValue = (int)vatRate;

            decimal calculatedNet = net > 0 ? net.Value : (gross > 0 ? gross.Value / (1 + vatRateValue / 100) : vat.Value / (vatRateValue / 100));
            decimal calculatedGross = gross > 0 ? gross.Value : (net > 0 ? net.Value * (1 + vatRateValue / 100) : vat.Value + calculatedNet);
            decimal calculatedVat = vat > 0 ? vat.Value : (gross > 0 ? gross.Value - calculatedNet : calculatedNet * vatRateValue / 100);

            return new VatCalculation
            {
                Net = calculatedNet,
                Gross = calculatedGross,
                Vat = calculatedVat,
                VatRate = vatRate
            };
        }
    }
}
