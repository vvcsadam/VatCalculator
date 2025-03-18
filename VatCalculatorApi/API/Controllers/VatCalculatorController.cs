using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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

        [HttpPost]
        public IActionResult Calculate([FromBody] VatCalculationDto vatCalculationDto)
        {
            try
            {
                var result = _vatCalculatorService.CalculateVat(vatCalculationDto);
                _logger.LogInformation(string.Format("Returning success response: {0}", JsonSerializer.Serialize(result)));

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                _logger.LogError(e, e.Message, vatCalculationDto);
                return BadRequest(new { error = e.Message });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message, vatCalculationDto);
                return BadRequest(new { error = "An unexpected error occurred while calculating." });
            }
        }
    }
}
