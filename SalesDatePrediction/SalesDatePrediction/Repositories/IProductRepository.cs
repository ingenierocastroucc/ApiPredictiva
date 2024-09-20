using SalesDatePrediction.Models;
using System.Threading;

namespace SalesDatePrediction.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken);
    }
}
