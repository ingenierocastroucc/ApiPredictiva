using SalesDatePrediction.Context;
using SalesDatePrediction.Models;
using Microsoft.EntityFrameworkCore;


namespace SalesDatePrediction.Repositories
{
    public class ProductsRepository : IProductRepository
    {
        private readonly SalesContext _context;

        public ProductsRepository(SalesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            // Consultar la base de datos para obtener la lista de productos
            return await _context.ProductlVirtual.ToListAsync();
        }
    }
}
