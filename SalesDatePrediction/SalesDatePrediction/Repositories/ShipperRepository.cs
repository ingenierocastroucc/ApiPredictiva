using SalesDatePrediction.Context;
using SalesDatePrediction.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesDatePrediction.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly SalesContext _context;

        public ShipperRepository(SalesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shipper>> GetShippersAsync()
        {
            // Consultar la base de datos para obtener la lista de productos
            return await _context.Shippers.ToListAsync();
        }
    }
}
