using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Models.Models;
using SalesDatePrediction.Repositories;
using System.Net;

namespace SalesDatePrediction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _orderRepository;

        public OrdersController(IOrdersRepository orderRepository)
        {
            _orderRepository = orderRepository;
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