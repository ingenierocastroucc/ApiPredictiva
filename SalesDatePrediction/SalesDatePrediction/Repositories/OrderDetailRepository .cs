using Microsoft.EntityFrameworkCore;
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

    public async Task AddOrderDetailAsync(OrderDetails orderDetail, CancellationToken cancellationToken)
    {
        if (orderDetail == null)
        {
            throw new ArgumentNullException(nameof(orderDetail), "El detalle de la orden no puede ser nulo.");
        }

        await _context.OrderDetails.AddAsync(orderDetail);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Error al agregar el detalle de la orden en la base de datos.", ex);
        }
    }
}
