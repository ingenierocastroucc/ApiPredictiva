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

        public OrdersController(IOrdersRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrdersByCustomer(int customerId)
        {
            var orders = await _orderRepository.GetOrdersAsync(customerId);
            return Ok(orders);
        }
    }

}
