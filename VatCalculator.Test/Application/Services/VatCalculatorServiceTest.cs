using Moq;
using VatCalculator.Api.Application.Interfaces;
using VatCalculator.Api.Application.Mappers;
using VatCalculatorApi.Application.DTO;
using VatCalculatorApi.Application.Interfaces;
using VatCalculatorApi.Application.Services;
using VatCalculatorApi.Domain.Entities;

namespace VatCalculator.Test.Application.Services
{
    public class VatCalculatorServiceTest
    {
        private readonly IVatCalculatorService _vatCalculatorService;
        private readonly IValidator<VatCalculation> _validator;
        private readonly IMapper<VatCalculationDto, VatCalculation> _mapper;

        public VatCalculatorServiceTest()
        {
            _validator = new Mock<IValidator<VatCalculation>>().Object;
            _mapper = new VatCalculationDtoMapper();
            _vatCalculatorService = new VatCalculatorService(_validator, _mapper);
        }

        [Theory]
        [InlineData(100, 0, 0, 20, 100, 120, 20, 20)]
        [InlineData(0, 120, 0, 20, 100, 120, 20, 20)]
        [InlineData(0, 0, 20, 20, 100, 120, 20, 20)]
        public void CalculateVat_WithNetProvided_CalculatesCorrectValues(
            decimal net, decimal gross, decimal vat, int vatRate,
            decimal expectedNet, decimal expectedGross, decimal expectedVat, int expectedVatRate)
        {
            var vatCalculationDto = new VatCalculationDto
            {
                Net = net,
                Gross = gross,
                Vat = vat,
                VatRate = vatRate
            };

            // Act
            var result = _vatCalculatorService.CalculateVat(vatCalculationDto);

            // Assert
            Assert.Equal(expectedNet, result.Net);
            Assert.Equal(expectedGross, result.Gross);
            Assert.Equal(expectedVat, result.Vat);
            Assert.Equal(expectedVatRate, result.VatRate);
        }
    }
}
