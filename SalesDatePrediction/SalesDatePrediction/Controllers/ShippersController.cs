using SalesDatePrediction.Models;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Repositories;
using Microsoft.Extensions.Logging;

namespace SalesDatePrediction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippersController : ControllerBase
    {
        private readonly IShipperRepository _shipperRepository;
        private readonly ILogger<ShippersController> _logger;

        public ShippersController(IShipperRepository shipperRepository, ILogger<ShippersController> logger)
        {
            _shipperRepository = shipperRepository;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene una lista de transportistas.
        /// </summary>
        /// <returns>Una lista de transportistas.</returns>
        [HttpGet]
        public async Task<IActionResult> GetShippers(CancellationToken cancellationToken)
        {
            try
            {
                var shippers = await _shipperRepository.GetShippersAsync(cancellationToken);
                if (shippers == null || !shippers.Any())
                {
                    _logger.LogWarning("No se encontraron transportistas.");
                    return NotFound(new { Mensaje = "No se encontraron transportistas." });
                }

                return Ok(shippers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al recuperar los transportistas.");
                return StatusCode(500, new { Mensaje = "Ocurrió un error al procesar su solicitud." });
            }
        }
    }
}