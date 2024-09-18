using SalesDatePrediction.Models;

namespace SalesDatePrediction.Repositories
{
    public interface ICustomersRepository
    {
        Task<IEnumerable<Customers>> GetCustomersAsync();
    }

}
