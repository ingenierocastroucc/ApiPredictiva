using SalesDatePrediction.Models;

namespace SalesDatePrediction.Repositories
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employees>> GetEmployeesAsync();
    }

}
