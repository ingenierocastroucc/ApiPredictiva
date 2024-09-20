using SalesDatePrediction.Context;
using SalesDatePrediction.Models.Models;

namespace SalesDatePrediction.Repositories
{
    public class CreateOrderRepository : ICreateOrderRepository
    {
        private readonly SalesContext _context;

        public CreateOrderRepository(SalesContext context)
        {
            _context = context;
        }

        public async Task<Orders> CreateOrderAsync(Orders order, CancellationToken cancellationToken)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
