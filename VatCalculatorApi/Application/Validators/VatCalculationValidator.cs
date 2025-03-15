using VatCalculatorApi.Domain.Entities;
using VatCalculatorApi.Domain.Enums;

namespace VatCalculatorApi.Application.Validators
{
    public static class VatCalculationValidator
    {
        public static void Validate(VatCalculation vatCalculation)
        {
            if (vatCalculation == null)
                throw new ArgumentNullException(nameof(vatCalculation), "VAT calculation cannot be null.");

            if (Enum.IsDefined(typeof(VatRate), vatCalculation.VatRate))
                throw new ArgumentException("Invalid VAT rate. Allowed values: 10%, 13%, 20%.");

            int providedValuesCount = 0;
            if (vatCalculation.Net > 0) providedValuesCount++;
            if (vatCalculation.Gross > 0) providedValuesCount++;
            if (vatCalculation.Vat > 0) providedValuesCount++;

            if (providedValuesCount != 1)
                throw new ArgumentException("You must provide exactly one value: Net, Gross, or VAT.");

            if (vatCalculation.Net < 0 || vatCalculation.Gross < 0 || vatCalculation.Vat < 0)
                throw new ArgumentException("Amounts cannot be negative.");
        }
    }
}
