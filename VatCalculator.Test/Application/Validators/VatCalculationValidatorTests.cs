using VatCalculatorApi.Application.Interfaces;
using VatCalculatorApi.Application.Validators;
using VatCalculatorApi.Domain.Entities;
using VatCalculatorApi.Domain.Enums;

namespace VatCalculator.Test.Application.Validators
{
    public class VatCalculationValidatorTests
    {
        private readonly IValidator<VatCalculation> _validator;

        public VatCalculationValidatorTests()
        {
            _validator = new VatCalculationValidator();
        }

        [Fact]
        public void Validate_ShouldThrowArgumentNullException_WhenVatCalculationIsNull()
        {
            // Arrange
            VatCalculation vatCalculation = null;

            // Act
            var exception = Assert.Throws<ArgumentNullException>(() => _validator.Validate(vatCalculation));

            //Assert
            Assert.Equal("vatCalculation", exception.ParamName);
        }

        [Fact]
        public void Validate_ShouldThrowArgumentException_WhenVatRateIsInvalid()
        {
            // Arrange
            var vatCalculation = new VatCalculation
            {
                VatRate = 123,
                Net = 100
            };

            // Act
            var exception = Assert.Throws<ArgumentException>(() => _validator.Validate(vatCalculation));

            //Assert
            Assert.Equal("Invalid VAT rate. Allowed values: 10%, 13%, 20%.", exception.Message);
        }

        [Fact]
        public void Validate_ShouldNotThrowException_WhenVatRateIsValid()
        {
            // Arrange
            var vatCalculation = new VatCalculation
            {
                VatRate = (int)VatRate.Vat10,
                Net = 100
            };

            // Act
            var exception = Record.Exception(() => _validator.Validate(vatCalculation));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Validate_ShouldThrowArgumentException_WhenMoreThanOneValueIsProvided()
        {
            // Arrange
            var vatCalculation = new VatCalculation
            {
                Net = 100,
                Gross = 120,
                VatRate = (int)VatRate.Vat20
            };

            // Act
            var exception = Assert.Throws<ArgumentException>(() => _validator.Validate(vatCalculation));

            //Assert
            Assert.Equal("You must provide exactly one positive value: Net, Gross, or VAT.", exception.Message);
        }

        [Fact]
        public void Validate_ShouldThrowArgumentException_WhenNoValuesAreProvided()
        {
            // Arrange
            var vatCalculation = new VatCalculation
            {
                Net = 0,
                Gross = 0,
                Vat = 0,
                VatRate = (int)VatRate.Vat20
            };

            // Act
            var exception = Assert.Throws<ArgumentException>(() => _validator.Validate(vatCalculation));

            //Assert
            Assert.Equal("You must provide exactly one positive value: Net, Gross, or VAT.", exception.Message);
        }

        [Theory]
        [InlineData(-1, 0, 0)]
        [InlineData(0, -1, 0)]
        [InlineData(0, 0, -1)]
        public void Validate_ShouldThrowArgumentException_WhenNetOrGrossOrVatIsNegative(decimal net, decimal gross, decimal vat)
        {
            // Arrange
            var vatCalculation = new VatCalculation
            {
                Net = net,
                Gross = gross,
                Vat = vat,
                VatRate = (int)VatRate.Vat20
            };

            // Act
            var exception = Assert.Throws<ArgumentException>(() => _validator.Validate(vatCalculation));

            //Assert
            Assert.Equal("You must provide exactly one positive value: Net, Gross, or VAT.", exception.Message);
        }

        [Theory]
        [InlineData(100, 0, 0)]
        [InlineData(0, 100, 0)]
        [InlineData(0, 0, 10.5)]
        public void Validate_ShouldNotThrowException_WhenValidNetOrGrossOrVatCalculationIsProvided(decimal net, decimal gross, decimal vat)
        {
            // Arrange
            var vatCalculation = new VatCalculation
            {
                Net = net,
                Gross = gross,
                Vat = vat,
                VatRate = (int)VatRate.Vat20
            };

            // Act
            var exception = Record.Exception(() => _validator.Validate(vatCalculation));

            //Assert
            Assert.Null(exception);
        }
    }
}
