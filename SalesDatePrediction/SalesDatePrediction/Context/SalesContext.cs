#region Documentación
/**************************************************************************************************** 
* API REST                                                      
**************************************************************************************************** 
* Unidad        : .NET/C# para el contexto y la creación de data                                                                      
* DescripciÓn   : Lógica de negocio para el contexto y la creación de data                                                       
* Autor         : Pedro Castro 
* Fecha         : 07-09-2024 
***************************************************************************************************/
#endregion Documentación

using SalesDatePrediction.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using SalesDatePrediction.Models.Models;

namespace SalesDatePrediction.Context
{
    public class SalesContext : DbContext
    {
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipper> Shippers { get; set; }

        public SalesContext(DbContextOptions<SalesContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureCustomers(modelBuilder);
            ConfigureOrders(modelBuilder);
            ConfigureOrderDetails(modelBuilder);
            ConfigureEmployees(modelBuilder);
            ConfigureProducts(modelBuilder);
            ConfigureShippers(modelBuilder);
        }

        public void ConfigureCustomers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customers>(entity =>
            {
                entity.ToTable("Customers");
                entity.HasKey(c => c.CustomerId);
                entity.Property(c => c.CustomerName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.LastOrderDate);
                entity.Property(c => c.NextPredictedOrder);
            });
        }

        public void ConfigureOrders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(o => o.OrderId);
                entity.Property(o => o.CustomerId).IsRequired();
                entity.Property(o => o.EmpId).IsRequired();
                entity.Property(o => o.ShipName).HasMaxLength(100);
                entity.Property(o => o.ShipAddress).HasMaxLength(250);
                entity.Property(o => o.ShipCity).HasMaxLength(100);
                entity.Property(o => o.OrderDate).IsRequired();
                entity.Property(o => o.RequireDdate);
                entity.Property(o => o.ShippedDate);
                entity.Property(o => o.Freight);
                entity.Property(o => o.ShipCountry).HasMaxLength(100);
            });
        }

        public void ConfigureOrderDetails(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.ToTable("OrderDetails");
                entity.HasKey(od => new { od.OrderId, od.ProductId });
                entity.Property(od => od.UnitPrice);
                entity.Property(od => od.Qty);
                entity.Property(od => od.Discount);
            });
        }

        public void ConfigureEmployees(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employees>(entity =>
            {
                entity.ToTable("Employees");
                entity.HasKey(e => e.EmpId);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            });
        }

        public void ConfigureProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
            });
        }

        public void ConfigureShippers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipper>(entity =>
            {
                entity.ToTable("Shippers");
                entity.HasKey(s => s.ShipperId);
                entity.Property(s => s.CompanyName).IsRequired().HasMaxLength(100);
            });
        }
    }
}
