#region Documentación
/****************************************************************************************************
* API REST                                                      
****************************************************************************************************
* Unidad        : <.NET/C# para el contexto y la creacion de data>                                                                      
* DescripciÓn   : <Logica de negocio para el contexto y la creacion de data>                                                      
* Autor         : <Pedro Castro>
* Fecha         : <07-09-2024>                                                                             
***************************************************************************************************/
#endregion Documentación

using SalesDatePrediction.Models;
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Models.Models;

namespace SalesDatePrediction.Context
{
    public class SalesContext : DbContext
    {
        /// <summary>
        /// Propiedad para la obtencion del Dbset de los Customers
        /// </summary>
        public DbSet<Customers> CustomersVirtual { get; set; }

        /// <summary>
        /// Propiedad para la obtencion del Dbset de los Employees
        /// </summary>
        public DbSet<Employees> EmployeesVirtual { get; set; }

        /// <summary>
        /// Propiedad para la obtencion del Dbset de las Orders
        /// </summary>
        public DbSet<Orders> OrdersVirtual { get; set; }

        /// <summary>
        /// Propiedad para la obtencion del Dbset de las OrderDetails
        /// </summary>
        public DbSet<OrderDetails> OrdersDetailVirtual { get; set; }

        /// <summary>
        /// Propiedad para la obtencion del Dbset de los Product
        /// </summary>
        public DbSet<Product> ProductlVirtual { get; set; }

        /// <summary>
        /// Propiedad para la obtencion del Dbset de los Shipper
        /// </summary>
        public DbSet<Shipper> ShipperVirtual { get; set; }

        /// <summary>
        /// Propiedad para el seteo en la base de datos
        /// </summary>
        public SalesContext(DbContextOptions<SalesContext> options) : base(options) { }

        // Método para configurar el modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Customers> customersInit = new List<Customers>();

            modelBuilder.Entity<Customers>(customers =>
            {
                customers.ToTable("Customers");
                customers.HasKey(p => p.CustomerId);
                customers.Property(p => p.CustomerName);
                customers.Property(p => p.LastOrderDate);
                customers.Property(p => p.NextPredictedOrder);
                customers.HasData(customersInit);
            }
            );

            List<Orders> ordersInit = new List<Orders>();

            modelBuilder.Entity<Orders>(orders =>
            {
                orders.ToTable("Orders");
                orders.HasKey(p => p.OrderId);
                orders.Property(p => p.CustomerId);
                orders.Property(p => p.EmpId);
                orders.Property(p => p.ShipName);
                orders.Property(p => p.ShipAddress);
                orders.Property(p => p.ShipCity);
                orders.Property(p => p.OrderDate);
                orders.Property(p => p.RequireDdate);
                orders.Property(p => p.ShippedDate);
                orders.Property(p => p.Freight);
                orders.Property(p => p.ShipCountry);
                orders.HasData(ordersInit);
            });

            List<OrderDetails> ordersDetailInit = new List<OrderDetails>();

            modelBuilder.Entity<OrderDetails>(orderDetails =>
            {
                orderDetails.ToTable("OrderDetails");
                orderDetails.HasKey(od => od.OrderId);
                orderDetails.Property(od => od.ProductId);
                orderDetails.Property(od => od.UnitPrice);
                orderDetails.Property(od => od.Qty);
                orderDetails.Property(od => od.Discount);
                orderDetails.HasData(ordersDetailInit);
            });

            List<Employees> employeesInit = new List<Employees>();

            modelBuilder.Entity<Employees>(employees =>
            {
                employees.ToTable("Employees");
                employees.HasKey(p => p.EmpId);
                employees.Property(p => p.FirstName);
                employees.Property(p => p.LastName);
                employees.HasData(employeesInit);
            }
            );

            List<Product> productInit = new List<Product>();

            modelBuilder.Entity<Product>(products =>
            {
                products.ToTable("Products");
                products.HasKey(p => p.ProductId);
                products.Property(p => p.ProductName);
                products.HasData(employeesInit);
            }
            );

            List<Shipper> ShipperInit = new List<Shipper>();

            modelBuilder.Entity<Shipper>(shipper =>
            {
                shipper.ToTable("Shippers");
                shipper.HasKey(p => p.ShipperId);
                shipper.Property(p => p.CompanyName);
                shipper.HasData(employeesInit);
            }
            );
        }
    }

}
