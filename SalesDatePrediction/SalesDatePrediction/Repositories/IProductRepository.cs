using SalesDatePrediction.Models;

namespace SalesDatePrediction.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
