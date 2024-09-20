#region Documentación
/**************************************************************************************************** 
* Endpoints:
* 1. GET api/Orders
*    - Descripción: Recupera una lista de todos las ordenes registradas.
*    - Respuestas:
*      - 200 OK: Devuelve una lista de clientes en formato JSON.
*      - 404 Not Found: No se encontraron ordenes.
*      - 500 Internal Server Error: Ocurrió un error en el servidor al procesar la solicitud.
***************************************************************************************************/
#endregion Documentación

using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Models.Models;
using SalesDatePrediction.Repositories;

namespace SalesDatePrediction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _orderRepository;
        private readonly ILogger<ProductsController> _logger;

        public OrdersController(IOrdersRepository orderRepository, ILogger<ProductsController> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene una lista de órdenes para un cliente específico.
        /// </summary>
        /// <param name="customerId">ID del cliente</param>
        /// <returns>Lista de órdenes</returns>
        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrdersByCustomer(int customerId)
        {
            if (customerId <= 0)
            {
                return BadRequest("El ID del cliente debe ser un número positivo.");
            }

            var orders = await _orderRepository.GetOrdersAsync(customerId);

            if (orders == null || !orders.Any())
            {
                _logger.LogWarning($"No se encontraron órdenes para el cliente con ID {customerId}.");
                return NotFound($"No se encontraron órdenes para el cliente con ID {customerId}.");
            }

            var orderDtos = orders.Select(order => new Orders // Mapea a DTO
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                ShipAddress = order.ShipAddress,
                ShipName = order.ShipName,
                ShipCity = order.ShipCity
            });

            return Ok(orderDtos);
        }
    }
}