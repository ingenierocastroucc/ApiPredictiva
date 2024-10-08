﻿#region Documentación
/**************************************************************************************************** 
* Endpoints:
* 1. GET api/employees
*    - Descripción: Recupera una lista de todos los empleados registrados.
*    - Respuestas:
*      - 200 OK: Devuelve una lista de clientes en formato JSON.
*      - 404 Not Found: No se encontraron empleados.
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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _employeeRepository;
        private readonly ILogger<ProductsController> _logger;

        public EmployeesController(IEmployeesRepository employeeRepository, ILogger<ProductsController> logger)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger;
        }

        /// <summary>
        /// Recupera una lista de empleados.
        /// </summary>
        /// <returns>Una lista de empleados.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Employees>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployees()
        {
            try
            {
                var employees = await _employeeRepository.GetEmployeesAsync();
                if (employees == null || !employees.Any())
                {   
                    _logger.LogWarning("No se encontraron empleados.");
                    return NotFound("No se encontraron empleados.");
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al recuperar los empleados.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al recuperar los empleados.");
            }
        }
    }
}