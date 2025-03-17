using Moq;
using VatCalculatorApi.Application.Interfaces;
using VatCalculatorApi.Application.Services;
using VatCalculatorApi.Domain.Entities;

namespace VatCalculator.Test.Application.Services
{
    public class VatCalculatorServiceTest
    {
        private readonly IVatCalculatorService _vatCalculatorService;
        private readonly IValidator<VatCalculation> _validator;

        public VatCalculatorServiceTest()
        {
            _validator = new Mock<IValidator<VatCalculation>>().Object;
            _vatCalculatorService = new VatCalculatorService(_validator);
        }

        [Theory]
        [InlineData(100, 0, 0, 20, 100, 120, 20, 20)]
        [InlineData(0, 120, 0, 20, 100, 120, 20, 20)]
        [InlineData(0, 0, 20, 20, 100, 120, 20, 20)]
        public void CalculateVat_WithNetProvided_CalculatesCorrectValues(
            decimal net, decimal gross, decimal vat, int vatRate,
            decimal expectedNet, decimal expectedGross, decimal expectedVat, int expectedVatRate)
        {
            // Act
            var result = _vatCalculatorService.CalculateVat(net, gross, vat, vatRate);

            // Assert
            Assert.Equal(expectedNet, result.Net);
            Assert.Equal(expectedGross, result.Gross);
            Assert.Equal(expectedVat, result.Vat);
            Assert.Equal(expectedVatRate, result.VatRate);
        }
    }
}
