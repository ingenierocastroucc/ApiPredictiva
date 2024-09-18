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

        public OrdersManagementController(ICreateOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request body cannot be null.");
            }

            if (request.Order == null || request.OrderDetails == null)
            {
                return BadRequest("Order and OrderDetails cannot be null.");
            }

            // Crea la orden
            var createdOrder = await _orderRepository.CreateOrderAsync(request.Order);

            // Asocia el ID de la orden a los detalles de la orden
            request.OrderDetails.OrderId = createdOrder.OrderId;

            // Guarda los detalles de la orden
            await _orderDetailRepository.AddOrderDetailAsync(request.OrderDetails);

            // Devuelve una respuesta creada con la nueva orden
            return CreatedAtAction(nameof(CreateOrder), new { id = createdOrder.OrderId }, createdOrder);
        }
    }

}
