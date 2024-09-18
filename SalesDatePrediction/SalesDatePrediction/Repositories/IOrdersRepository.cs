using SalesDatePrediction.Models;
using SalesDatePrediction.Models.Models;

namespace SalesDatePrediction.Repositories
{
    public interface IOrdersRepository
    {
        Task<IEnumerable<Orders>> GetOrdersAsync(int customerId);
    }

}
