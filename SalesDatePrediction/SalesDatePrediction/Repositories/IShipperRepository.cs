using SalesDatePrediction.Models;

namespace SalesDatePrediction.Repositories
{
    public interface IShipperRepository
    {
        Task<IEnumerable<Shipper>> GetShippersAsync(CancellationToken cancellationToken);
    }
}
