#region Documentación
/**************************************************************************************************** 
* Endpoints:
* 1. GET api/Products
*    - Descripción: Recupera una lista de todos los productos registrados.
*    - Respuestas:
*      - 200 OK: Devuelve una lista de productos en formato JSON.
*      - 404 Not Found: No se encontraron ordenes.
*      - 500 Internal Server Error: Ocurrió un error en el servidor al procesar la solicitud.
***************************************************************************************************/
#endregion Documentación
using SalesDatePrediction.Models;
using Microsoft.AspNetCore.Mvc;
using SalesDatePrediction.Repositories;

namespace SalesDatePrediction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Obteniendo productos del repositorio.");
                var products = await _productRepository.GetProductsAsync(cancellationToken);

                if (products == null || !products.Any())
                {
                    _logger.LogWarning("No se encontraron productos.");
                    return NotFound("No hay productos disponibles.");
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al obtener los productos.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error al consultar lso productos.");
            }
        }
    }
}
