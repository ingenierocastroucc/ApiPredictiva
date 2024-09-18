using SalesDatePrediction.Context;
using SalesDatePrediction.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesDatePrediction.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private readonly SalesContext _context;

        public EmployeesRepository(SalesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employees>> GetEmployeesAsync()
        {
            // Consultar la base de datos para obtener la lista de empleados
            return await _context.EmployeesVirtual.ToListAsync();
        }
    }

}
