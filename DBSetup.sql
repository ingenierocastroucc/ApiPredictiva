-- Creacion la base de datos SalesDatePrediction
CREATE DATABASE SalesDatePrediction;
GO

USE SalesDatePrediction;
GO

-- Creacion de tabla Clientes
CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY IDENTITY(1,1),    -- Identificador �nico auto-incrementable
    CustomerName NVARCHAR(100) NOT NULL          -- Nombre del cliente
);

-- La tabla Customers almacena la informaci�n b�sica de cada cliente.

-- Insertar datos en la tabla de Clientes
INSERT INTO Customers (CustomerName) VALUES
('Pedro Castro'),
('Karol Sevilla'),
('Rosa Castro');

-- Creacion de la tabla de Transportistas
CREATE TABLE Shippers (
    Shipperid INT PRIMARY KEY IDENTITY(1,1),     -- Identificador �nico auto-incrementable
    Companyname NVARCHAR(100) NOT NULL           -- Nombre de la empresa transportista
);

-- La tabla Shippers almacena informaci�n sobre las empresas de transporte.

-- Insertar datos en la tabla de Transportistas
INSERT INTO Shippers (Companyname) VALUES
('Envia Inc'),
('Transporte Ltd'),
('Express Ltd');

-- Creacion de la tabla de Empleados
CREATE TABLE Employees (
    Empid INT PRIMARY KEY IDENTITY(1,1),         -- Identificador �nico auto-incrementable
    Firstname NVARCHAR(50) NOT NULL,             -- Nombre del empleado
    Lastname NVARCHAR(50) NOT NULL               -- Apellido del empleado
);

-- La tabla Employees almacena informaci�n sobre los empleados.

-- Insertar datos en la tabla de Empleados
INSERT INTO Employees (Firstname, Lastname) VALUES
('Julian', 'Rodrigues'),
('Andres', 'Villa'),
('Luis', 'Causil');

-- Creacion de la tabla de Productos
CREATE TABLE Products (
    Productid INT PRIMARY KEY IDENTITY(1,1),      -- Identificador �nico auto-incrementable
    Productname NVARCHAR(100) NOT NULL,           -- Nombre del producto
	Quantity INT NOT NULL,
);

-- La tabla Products almacena informaci�n sobre los productos disponibles.

-- Insertar datos en la tabla de Productos
INSERT INTO Products (Productname, Quantity) VALUES
('Widget A', 10),
('Widget B', 20),
('Widget C', 30);

-- Crear tabla de �rdenes
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY,                     -- Identificador �nico para la orden
    CustomerId INT NOT NULL,                     -- Identificador del cliente que realiz� la orden
    Empid INT NOT NULL,                          -- Identificador del empleado que proces� la orden
    Shipperid INT NOT NULL,                      -- Identificador del transportista
    Shipname NVARCHAR(100) NOT NULL,             -- Nombre del destinatario del env�o
    Shipaddress NVARCHAR(255) NOT NULL,          -- Direcci�n del env�o
    Shipcity NVARCHAR(100) NOT NULL,             -- Ciudad de env�o
    Orderdate DATE NOT NULL,                     -- Fecha de la orden
    Requireddate DATE NOT NULL,                  -- Fecha requerida para la entrega
    Shippeddate DATE,                           -- Fecha en que se envi� la orden (puede ser NULL)
    Freight DECIMAL(18, 2) NOT NULL,            -- Costo del env�o
    Shipcountry NVARCHAR(100) NOT NULL,          -- Pa�s de destino
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),  -- Relaci�n con la tabla Customers
    FOREIGN KEY (Empid) REFERENCES Employees(Empid),            -- Relaci�n con la tabla Employees
    FOREIGN KEY (Shipperid) REFERENCES Shippers(Shipperid)      -- Relaci�n con la tabla Shippers
);
-- La tabla Orders almacena informaci�n detallada sobre cada orden.

-- Creacion de la tabla de Detalles de la Orden
CREATE TABLE OrderDetails (
    OrderId INT NOT NULL,                       -- Identificador de la orden
    Productid INT NOT NULL,                     -- Identificador del producto
    Unitprice DECIMAL(18, 2) NOT NULL,          -- Precio unitario del producto
    Qty INT NOT NULL,                           -- Cantidad del producto
    Discount DECIMAL(5, 2) DEFAULT 0.00,        -- Descuento aplicado al producto (valor por defecto 0)
    PRIMARY KEY (OrderId, Productid),           -- Clave primaria compuesta
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),         -- Relaci�n con la tabla Orders
    FOREIGN KEY (Productid) REFERENCES Products(Productid)    -- Relaci�n con la tabla Products
);
-- La tabla OrderDetails almacena los detalles de los productos en cada orden.


-- Insertar datos en la tabla de �rdenes
INSERT INTO Orders (OrderId, CustomerId, Empid, Shipperid, Shipname, Shipaddress, Shipcity, Orderdate, Requireddate, Shippeddate, Freight, Shipcountry) VALUES
(1, 1, 1, 1, 'John Doe', '123 Elm St', 'Springfield', '2024-09-01', '2024-09-10', '2024-09-05', 15.50, 'USA'),
(2, 2, 2, 2, 'Jane Smith', '456 Oak St', 'Shelbyville', '2024-08-25', '2024-09-05', '2024-08-30', 10.00, 'USA'),
(3, 3, 3, 3, 'Acme Corp', '789 Pine St', 'Capital City', '2024-08-30', '2024-09-15', NULL, 20.00, 'USA');

-- Insertar datos en la tabla de Detalles de la Orden
INSERT INTO OrderDetails (OrderId, Productid, Unitprice, Qty, Discount) VALUES
(1, 1, 25.00, 2, 0.05),
(2, 2, 15.00, 1, 0.10),
(3, 3, 30.00, 3, 0.00);

-- Predicci�n de la pr�xima fecha de pedido para cada cliente

USE SalesDatePrediction;
GO

-- Paso 1: Calcular la fecha del siguiente pedido para cada cliente
WITH OrderIntervals AS (
    SELECT 
        CustomerId,                                         -- Identificador del cliente
        OrderDate,                                          -- Fecha de la orden actual
        LEAD(OrderDate) OVER (PARTITION BY CustomerId ORDER BY OrderDate) AS NextOrderDate  -- Fecha del siguiente pedido para el mismo cliente
    FROM Orders
),
-- Paso 2: Calcular el intervalo en d�as entre pedidos consecutivos
IntervalDays AS (
    SELECT 
        CustomerId,                                         -- Identificador del cliente
        DATEDIFF(DAY, OrderDate, NextOrderDate) AS Interval -- Intervalo en d�as entre pedidos consecutivos
    FROM OrderIntervals
    WHERE NextOrderDate IS NOT NULL                      -- Considerar solo aquellos pedidos que tienen un siguiente pedido
),
-- Paso 3: Calcular el intervalo promedio entre pedidos para cada cliente
AvgInterval AS (
    SELECT 
        CustomerId,                                         -- Identificador del cliente
        AVG(Interval) AS AvgDaysBetweenOrders              -- Promedio de d�as entre pedidos para cada cliente
    FROM IntervalDays
    GROUP BY CustomerId                                  -- Agrupar por cliente para calcular el promedio
)
-- Paso 4: Predecir la fecha del siguiente pedido basado en el �ltimo pedido y el intervalo promedio
SELECT
    c.CustomerName,                                      -- Nombre del cliente
    MAX(o.OrderDate) AS LastOrderDate,                   -- Fecha del �ltimo pedido
    DATEADD(DAY, ISNULL(a.AvgDaysBetweenOrders, 30), MAX(o.OrderDate)) AS NextPredictedOrder  -- Fecha estimada del pr�ximo pedido
FROM Customers c
LEFT JOIN Orders o ON c.CustomerId = o.CustomerId        -- Unir con la tabla Orders para obtener la �ltima fecha de pedido
LEFT JOIN AvgInterval a ON c.CustomerId = a.CustomerId   -- Unir con la tabla AvgInterval para obtener el intervalo promedio
GROUP BY c.CustomerName, a.AvgDaysBetweenOrders;
