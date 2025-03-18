using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using VatCalculatorApi.API.Controllers;
using VatCalculatorApi.Application.DTO;
using VatCalculatorApi.Application.Interfaces;
using VatCalculatorApi.Domain.Entities;

namespace VatCalculator.Test.API.Controllers
{
    public class VatCalculatorControllerTest
    {
        private readonly Mock<IVatCalculatorService> _mockVatCalculatorService;
        private readonly Mock<ILogger<VatCalculatorController>> _mockLogger;
        private readonly VatCalculatorController _controller;

        public VatCalculatorControllerTest()
        {
            _mockVatCalculatorService = new Mock<IVatCalculatorService>();
            _mockLogger = new Mock<ILogger<VatCalculatorController>>();
            _controller = new VatCalculatorController(_mockLogger.Object, _mockVatCalculatorService.Object);
        }

        [Fact]
        public void Calculate_ShouldReturnOkResult_WhenCalculationIsSuccessful()
        {
            // Arrange
            var vatCalculationDto = new VatCalculationDto
            {
                Net = 11,
                Gross = 22,
                Vat = 33,
                VatRate = 44
            };

            var expectedCalculationResult = new VatCalculation
            {
                Net = 11,
                Gross = 22,
                Vat = 33,
                VatRate = 44
            };

            _mockVatCalculatorService
                .Setup(service => service.CalculateVat(vatCalculationDto))
                .Returns(expectedCalculationResult);

            // Act
            var result = _controller.Calculate(vatCalculationDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedCalculationResult, okResult.Value);
        }

        [Fact]
        public void Calculate_ShouldReturnBadRequest_WhenArgumentExceptionIsThrown()
        {
            // Arrange
            _mockVatCalculatorService
                .Setup(service => service.CalculateVat(new VatCalculationDto()))
                .Throws(new ArgumentException("Test error message"));

            // Act
            var result = _controller.Calculate(new VatCalculationDto());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Calculate_ShouldReturnBadRequest_WhenUnexpectedExceptionIsThrown()
        {
            // Arrange
            var vatCalculationDto = new VatCalculationDto
            {
                Net = 11,
                Gross = 22,
                Vat = 33,
                VatRate = 44
            };

            _mockVatCalculatorService
                .Setup(service => service.CalculateVat(vatCalculationDto))
                .Throws(new Exception("Test error message"));

            // Act
            var result = _controller.Calculate(vatCalculationDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
