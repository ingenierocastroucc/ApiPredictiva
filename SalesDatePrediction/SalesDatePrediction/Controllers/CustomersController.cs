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

        public CustomersController(ICustomersRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
        {
            var customers = await _customerRepository.GetCustomersAsync();
            return Ok(customers);
        }
    }

}
