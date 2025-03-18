using System;
using System.Collections.Generic;
using VatCalculator.Api.Application.Mappers;
using VatCalculatorApi.Application.DTO;
using VatCalculatorApi.Domain.Entities;
using FluentAssertions;
using VatCalculator.Api.Application.Interfaces;

namespace VatCalculator.Test.Application.Mappers
{
    public class VatCalculationDtoMapperTests
    {
        private readonly IMapper<VatCalculationDto, VatCalculation> _mapper;

        public VatCalculationDtoMapperTests()
        {
            _mapper = new VatCalculationDtoMapper();
        }

        [Fact]
        public void Map_ShouldMapAllProperties_Correctly()
        {
            // Arrange
            var dto = new VatCalculationDto { Net = 11, Gross = 22, Vat = 33, VatRate = 44 };
            var entity = new VatCalculation();

            // Act
            _mapper.Map(dto, entity);

            // Assert
            entity.Net.Should().Be(11);
            entity.Gross.Should().Be(22);
            entity.Vat.Should().Be(33);
            entity.VatRate.Should().Be(44);
        }

        [Fact]
        public void Map_WhenNullPropertiesInDto_SetDefaultValues()
        {
            // Arrange
            var dto = new VatCalculationDto();
            var entity = new VatCalculation();

            // Act
            _mapper.Map(dto, entity);

            // Assert
            entity.Net.Should().Be(0);
            entity.Gross.Should().Be(0);
            entity.Vat.Should().Be(0);
            entity.VatRate.Should().Be(-1);
        }
    }
}
