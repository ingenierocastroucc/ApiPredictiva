using SalesDatePrediction.Models;
using SalesDatePrediction.Repositories;
using SalesDatePrediction.Context;
using Microsoft.EntityFrameworkCore;

namespace SalesDatePrediction.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly SalesContext _context;

        public CustomersRepository(SalesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customers>> GetCustomersAsync()
        {
            // Primero, obtén todos los pedidos.
            var orders = await _context.OrdersVirtual
                .Select(o => new
                {
                    o.CustomerId,
                    o.OrderDate
                })
                .ToListAsync(); // Obtén la lista de pedidos en memoria

            // Obtén todos los clientes.
            var customers = await _context.CustomersVirtual
                .Select(c => new
                {
                    c.CustomerId,
                    c.CustomerName
                })
                .ToListAsync(); // Obtén la lista de clientes en memoria

            // Calcula el intervalo entre pedidos.
            var orderIntervals = orders
                .GroupBy(o => o.CustomerId)
                .SelectMany(group =>
                {
                    var ordersList = group.OrderBy(o => o.OrderDate).ToList();
                    return ordersList
                        .Select((o, index) => new
                        {
                            o.CustomerId,
                            OrderDate = o.OrderDate,
                            NextOrderDate = index < ordersList.Count - 1 ? ordersList[index + 1].OrderDate : (DateTime?)null
                        })
                        .Where(oi => oi.NextOrderDate.HasValue)
                        .Select(oi => new
                        {
                            oi.CustomerId,
                            Interval = (oi.NextOrderDate.Value - oi.OrderDate).Days
                        });
                });

            // Calcula el promedio del intervalo entre pedidos.
            var avgIntervals = orderIntervals
                .GroupBy(oi => oi.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    AvgDaysBetweenOrders = g.Average(oi => oi.Interval)
                });

            // Calcula los resultados finales en memoria
            var result = customers.Select(c => new
            {
                c.CustomerId,
                c.CustomerName,
                LastOrderDate = orders
                    .Where(o => o.CustomerId == c.CustomerId)
                    .OrderByDescending(o => o.OrderDate)
                    .Select(o => o.OrderDate)
                    .FirstOrDefault(),
                AvgDaysBetweenOrders = avgIntervals
                    .Where(a => a.CustomerId == c.CustomerId)
                    .Select(a => a.AvgDaysBetweenOrders)
                    .FirstOrDefault()
            });

            // Proyecta los resultados a la entidad Customers.
            var finalResult = result.Select(r => new Customers
            {
                CustomerId = r.CustomerId,
                CustomerName = r.CustomerName,
                LastOrderDate = r.LastOrderDate, // No necesita conversión
                NextPredictedOrder = r.LastOrderDate != default(DateTime)
                    ? r.LastOrderDate.AddDays(r.AvgDaysBetweenOrders > 0 ? r.AvgDaysBetweenOrders : 30)
                    : (DateTime?)null
            });

            return finalResult;
        }
    }

}
