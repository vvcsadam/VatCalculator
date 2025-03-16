using VatCalculatorApi.Domain.Enums;

namespace VatCalculatorApi.Application.DTO
{
    public class VatCalculationDto
    {
        public decimal? Net { get; set; }
        public decimal? Gross { get; set; }
        public decimal? Vat { get; set; }
        public int? VatRate { get; set; }
    }
}
