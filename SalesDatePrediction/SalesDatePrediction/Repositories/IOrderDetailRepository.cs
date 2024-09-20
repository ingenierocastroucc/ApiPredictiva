using SalesDatePrediction.Models;

namespace SalesDatePrediction.Repositories
{
    public interface IOrderDetailRepository
    {
        Task AddOrderDetailAsync(OrderDetails orderDetails, CancellationToken cancellationToken);
    }
}
