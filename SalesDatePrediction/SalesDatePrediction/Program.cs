using SalesDatePrediction.Repositories;
using Microsoft.EntityFrameworkCore;
using SalesDatePrediction.Context;
using SalesDatePrediction.Models.Models;
using SalesDatePrediction.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configuración de la base de datos
builder.Services.AddSqlServer<SalesContext>("Data Source=LAPTOP-PH1R9POH;Initial Catalog=SalesDatePrediction;Integrated Security=True;TrustServerCertificate=True;");

// Repositorios
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
builder.Services.AddScoped<ICreateOrderRepository, CreateOrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IProductRepository, ProductsRepository>();
builder.Services.AddScoped<IShipperRepository, ShipperRepository>();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin() // Permite cualquier origen, ajusta esto en producción
                         .AllowAnyHeader()
                         .AllowAnyMethod();
        });
});
var app = builder.Build();

// Aplicar la política de CORS antes de usar cualquier middleware que maneje solicitudes
app.UseCors("AllowSpecificOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();