using VatCalculator.Api.Application.Interfaces;
using VatCalculatorApi.Application.DTO;
using VatCalculatorApi.Application.Interfaces;
using VatCalculatorApi.Domain.Entities;

namespace VatCalculatorApi.Application.Services
{
    public class VatCalculatorService : IVatCalculatorService
    {
        private readonly IValidator<VatCalculation> _validator;
        private readonly IMapper<VatCalculationDto, VatCalculation> _mapper;

        public VatCalculatorService(IValidator<VatCalculation> validator, IMapper<VatCalculationDto, VatCalculation> mapper) 
        { 
            _validator = validator;
            _mapper = mapper;
        }

        public VatCalculation CalculateVat(VatCalculationDto vatCalculationDto)
        {
            var vatCalculation = new VatCalculation();
            _mapper.Map(vatCalculationDto, vatCalculation);

            _validator.Validate(vatCalculation);

            var calculatedNet = vatCalculation.Net > 0 ? vatCalculation.Net : (vatCalculation.Gross > 0 ? vatCalculation.Gross / (1 + (decimal)vatCalculation.VatRate / 100) : (vatCalculation.Vat / ((decimal)vatCalculation.VatRate / 100)));
            var calculatedGross = vatCalculation.Gross > 0 ? vatCalculation.Gross : (vatCalculation.Net > 0 ? vatCalculation.Net * (1 + (decimal)vatCalculation.VatRate / 100) : vatCalculation.Vat + calculatedNet);
            var calculatedVat = vatCalculation.Vat > 0 ? vatCalculation.Vat : (vatCalculation.Gross > 0 ? vatCalculation.Gross - calculatedNet : calculatedNet * (decimal)vatCalculation.VatRate / 100);

            vatCalculation.Net = calculatedNet;
            vatCalculation.Gross = calculatedGross;
            vatCalculation.Vat = calculatedVat;

            return vatCalculation;
        }
    }
}
