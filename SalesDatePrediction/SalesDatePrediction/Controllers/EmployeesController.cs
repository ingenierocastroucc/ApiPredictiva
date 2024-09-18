using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Models;
using SalesDatePrediction.Repositories;

namespace SalesDatePrediction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _employeeRepository;

        public EmployeesController(IEmployeesRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployees()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            return Ok(employees);
        }
    }

}
