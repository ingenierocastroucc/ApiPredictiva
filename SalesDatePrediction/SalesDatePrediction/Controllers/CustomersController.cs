#region Documentación
/**************************************************************************************************** 
* Endpoints:
* 1. GET api/customers
*    - Descripción: Recupera una lista de todos los clientes registrados.
*    - Respuestas:
*      - 200 OK: Devuelve una lista de clientes en formato JSON.
*      - 404 Not Found: No se encontraron clientes.
*      - 500 Internal Server Error: Ocurrió un error en el servidor al procesar la solicitud.
***************************************************************************************************/
#endregion Documentación
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Models;
using SalesDatePrediction.Repositories;

namespace SalesDatePrediction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository _customerRepository;
        private readonly ILogger<OrdersManagementController> _logger;

        public CustomersController(ICustomersRepository customerRepository, ILogger<OrdersManagementController> logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;
        }

        /// <summary>
        /// Recupera una lista de clientes.
        /// </summary>
        /// <returns>Una lista de clientes.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers(CancellationToken cancellationToken)
        {
            try
            {
                var customers = await _customerRepository.GetCustomersAsync(cancellationToken);
                if (customers == null || !customers.Any())
                {
                    _logger.LogWarning("No se encontraron clientes.");
                    return NotFound("No se encontraron clientes.");
                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al obtener el listado de clientes.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al recuperar los clientes. ");
            }
        }
    }
}
