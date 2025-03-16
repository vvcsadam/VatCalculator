using Microsoft.AspNetCore.Mvc;
using VatCalculatorApi.Application.DTO;
using VatCalculatorApi.Application.Interfaces;

namespace VatCalculatorApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatCalculatorController : ControllerBase
    {
        private readonly IVatCalculatorService _vatCalculatorService;
        private readonly ILogger<VatCalculatorController> _logger;

        public VatCalculatorController(ILogger<VatCalculatorController> logger, IVatCalculatorService vatCalculatorService)
        {
            _logger = logger;
            _vatCalculatorService = vatCalculatorService;
        }

        [HttpPost()]
        public IActionResult Calculate([FromBody] VatCalculationDto request)
        {
            try
            {
                var result = _vatCalculatorService.CalculateVat(request.Net, request.Gross, request.Vat, request.VatRate);
                _logger.LogInformation(string.Format("Returning success response"), request);

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, e.Message, request);
                return BadRequest(new { error = e.Message });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, request);
                return BadRequest(new { error = "An unexpected error occurred while calculating." });
            }
        }
    }
}
