using SalesDatePrediction.Context;
using SalesDatePrediction.Models;
using SalesDatePrediction.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesDatePrediction.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly SalesContext _context;

        public OrdersRepository(SalesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Orders>> GetOrdersAsync(int customerId)
        {
            // Suponiendo que el contexto tiene un DbSet<Orders> llamado Orders y una propiedad CustomerName en Orders
            return await _context.OrdersVirtual
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }
    }


}
