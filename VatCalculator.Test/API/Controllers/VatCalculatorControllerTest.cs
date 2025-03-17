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
            var request = new VatCalculationDto
            {
                Net = 100,
                Gross = 120,
                Vat = 20,
                VatRate = 20
            };

            var expectedCalculationResult = new VatCalculation
            {
                Net = 100,
                Gross = 120,
                Vat = 20,
                VatRate = 20
            };

            _mockVatCalculatorService
                .Setup(service => service.CalculateVat(request.Net, request.Gross, request.Vat, request.VatRate))
                .Returns(expectedCalculationResult);

            // Act
            var result = _controller.Calculate(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedCalculationResult, okResult.Value);
        }

        [Fact]
        public void Calculate_ShouldReturnBadRequest_WhenArgumentExceptionIsThrown()
        {
            // Arrange
            var request = new VatCalculationDto
            {
                Net = 100,
                Gross = 0,
                Vat = 20,
                VatRate = 20
            };

            _mockVatCalculatorService
                .Setup(service => service.CalculateVat(request.Net, request.Gross, request.Vat, request.VatRate))
                .Throws(new ArgumentException("Test error message"));

            // Act
            var result = _controller.Calculate(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Calculate_ShouldReturnBadRequest_WhenUnexpectedExceptionIsThrown()
        {
            // Arrange
            var request = new VatCalculationDto
            {
                Net = 100,
                Gross = 120,
                Vat = 20,
                VatRate = 20
            };

            _mockVatCalculatorService
                .Setup(service => service.CalculateVat(request.Net, request.Gross, request.Vat, request.VatRate))
                .Throws(new Exception("Test error message"));

            // Act
            var result = _controller.Calculate(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
