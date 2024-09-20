#region Documentación
/**************************************************************************************************** 
* Endpoints:
* 1. GET api/OrdersManagement
*    - Descripción: Crea un registro de una orden nueva con todos sus detalles.
*    - Respuestas:
*      - 200 OK: Devuelve una lista de la orden creada en formato JSON.
*      - 500 Internal Server Error: Ocurrió un error en el servidor al procesar la solicitud.
***************************************************************************************************/
#endregion Documentación
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Models.Models;
using SalesDatePrediction.Models;
using SalesDatePrediction.Repositories;

namespace SalesDatePrediction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersManagementController : ControllerBase
    {
        private readonly ICreateOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ILogger<OrdersManagementController> _logger;

        public OrdersManagementController(
            ICreateOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            ILogger<OrdersManagementController> logger)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Orders>> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                _logger.LogWarning("CreateOrder llamado con solicitud nula.");
                return BadRequest("El cuerpo de la solicitud no puede ser nulo.");
            }

            if (request.Order == null || request.OrderDetails == null)
            {
                _logger.LogWarning("CreateOrder llamado con orden o detalles de orden inválidos.");
                return BadRequest("La orden y los detalles de la orden no pueden ser nulos.");
            }

            try
            {
                // Crea la orden
                var createdOrder = await _orderRepository.CreateOrderAsync(request.Order, cancellationToken);

                // Asocia el ID de la orden a los detalles de la orden
                request.OrderDetails.OrderId = createdOrder.OrderId;

                // Guarda los detalles de la orden
                await _orderDetailRepository.AddOrderDetailAsync(request.OrderDetails, cancellationToken);

                // Devuelve una respuesta creada con la nueva orden
                return CreatedAtAction(nameof(CreateOrder), new { id = createdOrder.OrderId }, createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al crear la orden.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al registrar las ordenes.");
            }
        }
    }
}