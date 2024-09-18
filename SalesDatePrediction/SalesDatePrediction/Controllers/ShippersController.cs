using SalesDatePrediction.Models;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Repositories;

namespace SalesDatePrediction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippersController : ControllerBase
    {
        private readonly IShipperRepository _shipperRepository;

        public ShippersController(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shipper>>> GetShippers()
        {
            var shippers = await _shipperRepository.GetShippersAsync();
            return Ok(shippers);
        }
    }

}
