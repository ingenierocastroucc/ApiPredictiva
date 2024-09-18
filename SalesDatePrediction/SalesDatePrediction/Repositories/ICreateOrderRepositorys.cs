using SalesDatePrediction.Models.Models;

namespace SalesDatePrediction.Repositories
{
    public interface ICreateOrderRepository
    {
        Task<Orders> CreateOrderAsync(Orders order);
    }
}
