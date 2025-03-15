namespace VatCalculatorApi.Domain.Entities
{
    public class VatCalculation
    {
        public decimal Net { get; set; }
        public decimal Gross { get; set; }
        public decimal Vat { get; set; }
        public int VatRate { get; set; }
    }
}
