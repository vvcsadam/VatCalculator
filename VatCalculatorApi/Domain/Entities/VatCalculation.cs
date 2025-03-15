using VatCalculatorApi.Domain.Enums;

namespace VatCalculatorApi.Domain.Entities
{
    public class VatCalculation
    {
        public decimal Net { get; set; }
        public decimal Gross { get; set; }
        public decimal Vat { get; set; }
        public VatRate VatRate { get; set; }
    }
}
