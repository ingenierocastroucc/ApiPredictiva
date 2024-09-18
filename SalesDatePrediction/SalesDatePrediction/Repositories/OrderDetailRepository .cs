using SalesDatePrediction.Context;
using SalesDatePrediction.Models;
using SalesDatePrediction.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly SalesContext _context;

    public OrderDetailRepository(SalesContext context)
    {
        _context = context;
    }

    public async Task AddOrderDetailAsync(OrderDetails orderDetail)
    {
        _context.OrdersDetailVirtual.Add(orderDetail);
        await _context.SaveChangesAsync();
    }
}
