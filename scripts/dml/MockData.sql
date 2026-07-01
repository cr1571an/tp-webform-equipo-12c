USE COMERCIO_BBDD;
GO

/* CONDICION IVA - va primero porque Clientes depende de esta tabla */
INSERT INTO CondicionIva (Condicion) VALUES
('Responsable Inscripto'),
('Monotributo'),
('Consumidor Final'),
('Exento');
GO

/* CLIENTES */
INSERT INTO Clientes (IdCondicionIva, Cuit, Nombre, Apellido, Telefono, Email, Cp, Domicilio, Activo) VALUES
(3, NULL, 'Ana', 'Martínez', '1122223333', 'ana.martinez@mail.com', '1406', 'Av. Directorio 1234', 1),
(3, NULL, 'Juan', 'López', '1133334444', 'juan.lopez@mail.com', '1414', 'Gurruchaga 550', 1),
(2, '20-30111222-3', 'Carolina', 'Sosa', '1144445555', 'carolina.sosa@mail.com', '1602', 'Italia 780', 1),
(1, '30-70123456-7', 'Pet Hotel', 'Palermo', '1155556666', 'contacto@pethotel.com', '1425', 'Scalabrini Ortiz 2100', 1),
(3, NULL, 'Marcela', 'García', '1166667777', 'marcela.garcia@mail.com', '1706', 'Alsina 345', 1),
(2, '20-33444555-8', 'Florencia', 'Molina', '1188889999', 'flor.molina@mail.com', '1642', 'Maipú 1320', 1),
(3, NULL, 'Diego', 'Castro', '1199990000', 'diego.castro@mail.com', '1001', 'Corrientes 1500', 1),
(3, NULL, 'Valentina', 'Rojas', '1123456789', 'valentina.rojas@mail.com', '1878', 'Belgrano 875', 1),
(3, NULL, 'Pedro', 'Ramírez', '1177778888', 'pedro.ramirez@mail.com', '1870', 'San Juan 950', 0),
(2, '20-28777888-9', 'Laura', 'Benítez', '1143216789', 'laura.benitez@mail.com', '1617', 'Mitre 430', 0);
GO

/* EMPLEADOS */
INSERT INTO Empleados (Nombre, Apellido, Telefono, Email, Dni, FechaIngreso, Activo) VALUES
('Aldana Paula', 'Firpo', '1134567890', 'aldana.firpo@petshop.com', '30111222', '2023-03-10', 1),
('Ulises Sergio Martin', 'Aguirre', '1145678901', 'ulises.aguirre@petshop.com', '32123456', '2022-08-15', 1),
('Cristian Nicolas', 'Sanchez', '1156789012', 'cristian.sanchez@petshop.com', '33444555', '2024-01-20', 1),
('Javier Agustin', 'Larroca', '1167890123', 'javier.larroca@petshop.com', '28777888', '2021-11-05', 0),
('Maximiliano', 'Sar Fernández', '1178901234', 'maximiliano.fernandez@petshop.com', '35666777', '2020-06-12', 0);
GO

/* ROLES - todos activos */
INSERT INTO Roles (NombreRol, Descripcion, Activo) VALUES
('Administrador', 'Acceso total al sistema', 1),
('Vendedor', 'Puede registrar ventas y consultar artículos', 1),
('Compras', 'Puede registrar compras a proveedores', 1),
('Supervisor', 'Puede consultar reportes y controlar operaciones', 1);
GO

/* USUARIOS */
INSERT INTO Usuarios (IdEmpleado, IdRol, NombreUsuario, PasswordHash, Activo) VALUES
(1, 1, 'aldana', '123456', 1),
(2, 2, 'ulises', '123456', 1),
(3, 3, 'cristian', '123456', 1),
(4, 4, 'javier', '123456', 0),
(5, 2, 'maximiliano', '123456', 0);
GO

/* PROVEEDORES */
INSERT INTO Proveedores (Nombre, Cuit, Telefono, Email, Domicilio, Cp, PersonaContacto, Observaciones, Activo) VALUES
('Distribuidora Patitas SA', '30-71111111-1', '1130001111', 'ventas@patitas.com', 'Av. San Martín 1200', '1416', 'Mariana López', 'Entrega lunes y jueves', 1),
('Mundo Mascota SRL', '30-72222222-2', '1130002222', 'contacto@mundomascota.com', 'Calle Rivadavia 850', '1704', 'Pablo Díaz', NULL, 1),
('Alimentos Caninos Norte', '30-73333333-3', '1130003333', 'pedidos@caninosnorte.com', 'Ruta 8 Km 35', '1617', 'Laura Benítez', 'Consultar stock antes de comprar', 1),
('Pet Clean Mayorista', '30-74444444-4', '1130004444', 'info@petclean.com', 'Av. Belgrano 450', '1092', 'Diego Castro', NULL, 1),
('Accesorios Huellitas', '30-75555555-5', '1130005555', 'ventas@huellitas.com', 'Mitre 220', '1640', 'Camila Torres', NULL, 1),
('Sanitarios Felinos SRL', '30-76666666-6', '1130006666', 'pedidos@sanitariosfelinos.com', 'Sarmiento 900', '1000', 'Natalia Pérez', 'Proveedor de arenas y piedras sanitarias', 1),
('Nutrican Mayorista', '30-77777777-7', '1130007777', 'ventas@nutrican.com', 'Av. Córdoba 1540', '1055', 'Lucas Gómez', 'Actualmente sin convenio comercial', 0),
('ZooMarket Distribuciones', '30-78888888-8', '1130008888', 'info@zoomarket.com', 'Av. Entre Ríos 780', '1079', 'Andrea Silva', 'Precios desactualizados', 0);
GO

/* CATEGORIAS */
INSERT INTO Categorias (Nombre, Activo) VALUES
('Alimentos', 1),
('Juguetes', 1),
('Higiene', 1),
('Accesorios', 1),
('Salud', 1),
('Camas y cuchas', 1),
('Indumentaria', 0),
('Transportadoras', 0);
GO

/* MARCAS */
INSERT INTO Marcas (Nombre, Activo) VALUES
('Royal Canin', 1),
('Eukanuba', 1),
('Excellent', 1),
('VitalCan', 1),
('Pawise', 1),
('Whiskas', 1),
('Old Prince', 1),
('KONG', 0),
('Bayer', 0);
GO

/* ALICUOTA IVA - todas activas */
INSERT INTO AlicuotaIva (AlicuotaIva, Activo) VALUES
(0.00, 1),
(10.50, 1),
(21.00, 1),
(27.00, 1);
GO

/* MEDIOS DE PAGO */
INSERT INTO MediosPago (Nombre, Descripcion, Activo) VALUES
('Efectivo', 'Pago en efectivo', 1),
('Tarjeta de Débito', 'Pago con débito', 1),
('Tarjeta de Crédito', 'Pago con crédito', 1),
('Transferencia', 'Pago por transferencia bancaria', 1),
('Mercado Pago', 'Pago por billetera virtual', 1),
('Cuenta DNI', 'Pago con billetera virtual bancaria', 1),
('Cheque', 'Medio de pago actualmente no utilizado', 0),
('Crédito en cuenta corriente', 'Pago diferido para clientes con cuenta comercial', 0);
GO

/* ARTICULOS */
INSERT INTO Articulos (IdCategoria, IdAlicuotaIva, IdMarca, Nombre, Descripcion, PrecioUnitario, Stock, UrlImagen, Activo) VALUES
(1, 3, 1, 'Royal Canin Medium Adult 4kg', 'Alimento seco para perros adultos medianos', 28500, 20, 'https://www.mivetshop.com.ar/media/catalog/product/cache/b934b1cbb472a07065d94dead80b4c7d/7/7/7790187370487_01_1_1.jpg', 1),
(1, 3, 2, 'Eukanuba Adult Medium 15kg', 'Alimento balanceado para perros adultos medianos', 68500, 12, 'https://www.eukanuba.com/products/adult-dog-food/eukanuba-adult-dry-dog-food-medium-dogs', 1),
(1, 3, 3, 'Excellent Gato Adulto 1kg', 'Alimento seco para gato adulto', 7200, 35, NULL, 1),
(1, 3, 4, 'VitalCan Cachorro 3kg', 'Alimento para cachorro', 16200, 15, NULL, 1),
(1, 3, 6, 'Whiskas Adulto 1kg', 'Alimento seco para gato adulto', 6800, 28, NULL, 1),
(1, 3, 7, 'Old Prince Adulto 3kg', 'Alimento seco para perro adulto', 15400, 18, NULL, 1),
(2, 3, 8, 'KONG Classic Medium', 'Juguete mordillo resistente para perro', 14500, 12, 'https://catycanar.vtexassets.com/arquivos/ids/173860/7797453000987_00--1-.jpg?v=638986421130730000', 0),
(2, 3, 5, 'Pelota con sonido Pawise', 'Juguete para perro con sonido', 3200, 40, NULL, 1),
(3, 3, 5, 'Shampoo neutro 250ml', 'Shampoo para mascotas', 4500, 25, NULL, 1),
(3, 3, 6, 'Arena sanitaria 4kg', 'Arena sanitaria para gatos', 6100, 30, NULL, 1),
(4, 3, 5, 'Correa regulable Pawise', 'Correa regulable para perro', 5600, 18, NULL, 1),
(4, 3, 5, 'Collar reflectivo Pawise', 'Collar reflectivo para perro', 3900, 22, NULL, 1),
(5, 3, 9, 'Pipeta antipulgas Bayer', 'Antipulgas para perros medianos', 8500, 16, NULL, 0),
(6, 3, 5, 'Cama redonda chica', 'Cama para perro o gato', 12500, 8, NULL, 1),
(7, 3, 5, 'Buzo mascota talle M', 'Indumentaria para mascota', 9800, 5, NULL, 0);
GO

/* VENTAS */
INSERT INTO Ventas (IdCliente, IdUsuario, IdMedioPago, Fecha, NumeroFactura, Total, Activo) VALUES
(1, 2, 1, GETDATE(), 'V-0001-00000001', 28500, 1),
(2, 2, 5, GETDATE(), 'V-0001-00000002', 10300, 1),
(3, 2, 2, GETDATE(), 'V-0001-00000003', 12500, 1),
(4, 2, 4, GETDATE(), 'V-0001-00000004', 16200, 0),
(5, 2, 3, DATEADD(DAY,-1,GETDATE()), 'V-0001-00000005', 32400, 1),
(6, 2, 1, DATEADD(DAY,-2,GETDATE()), 'V-0001-00000006', 20200, 1),
(7, 2, 6, DATEADD(DAY,-3,GETDATE()), 'V-0001-00000007', 15400, 1),
(8, 2, 5, DATEADD(DAY,-4,GETDATE()), 'V-0001-00000008', 12500, 1),
(1, 2, 1, DATEADD(DAY,-5,GETDATE()), 'V-0001-00000009', 6100, 0),
(2, 2, 2, DATEADD(DAY,-6,GETDATE()), 'V-0001-00000010', 6800, 1),
(3, 2, 3, DATEADD(DAY,-7,GETDATE()), 'V-0001-00000011', 5600, 1),
(4, 2, 4, DATEADD(DAY,-8,GETDATE()), 'V-0001-00000012', 4500, 0);
GO

/* DETALLES VENTA */
INSERT INTO DetallesVenta (IdVenta, IdArticulo, Cantidad, PrecioUnitario, SubTotal) VALUES
(1, 1, 1, 28500, 28500),
(2, 8, 2, 3200, 6400),
(2, 12, 1, 3900, 3900),
(3, 14, 1, 12500, 12500),
(4, 4, 1, 16200, 16200),
(5, 4, 2, 16200, 32400),
(6, 3, 1, 7200, 7200),
(6, 9, 1, 4500, 4500),
(6, 13, 1, 8500, 8500),
(7, 6, 1, 15400, 15400),
(8, 14, 1, 12500, 12500),
(9, 10, 1, 6100, 6100),
(10, 5, 1, 6800, 6800),
(11, 11, 1, 5600, 5600),
(12, 9, 1, 4500, 4500);
GO

/* COMPRAS */
INSERT INTO Compras (IdUsuario, IdProveedor, IdMedioPago, FechaCompra, NumeroFactura,Observaciones, Total, Activo) VALUES
(3, 1, 4, GETDATE(), 'C-0001-00000001',NULL, 114000, 1),
(3, 2, 4, GETDATE(), 'C-0001-00000002',NULL, 137000, 1),
(3, 3, 1, GETDATE(), 'C-0001-00000003',NULL, 48600, 1),
(3, 4, 4, GETDATE(), 'C-0001-00000004',NULL, 31500, 1),
(3, 5, 5, GETDATE(), 'C-0001-00000005',NULL, 22400, 1),
(3, 6, 4, DATEADD(DAY,-1,GETDATE()), 'C-0001-00000006',NULL, 30500, 1),
(3, 1, 4, DATEADD(DAY,-2,GETDATE()), 'C-0001-00000007',NULL, 142500, 1),
(3, 2, 4, DATEADD(DAY,-3,GETDATE()), 'C-0001-00000008',NULL, 28800, 1),
(3, 3, 1, DATEADD(DAY,-4,GETDATE()), 'C-0001-00000009',NULL, 64800, 1),
(3, 4, 4, DATEADD(DAY,-5,GETDATE()), 'C-0001-00000010',NULL, 45000, 1),
(3, 5, 5, DATEADD(DAY,-6,GETDATE()), 'C-0001-00000011',NULL, 28000, 1),
(3, 6, 4, DATEADD(DAY,-7,GETDATE()), 'C-0001-00000012',NULL, 24400, 0),
(3, 1, 4, DATEADD(DAY,-8,GETDATE()), 'C-0001-00000013',NULL, 85500, 1),
(3, 2, 4, DATEADD(DAY,-9,GETDATE()), 'C-0001-00000014',NULL, 43200, 1),
(3, 3, 1, DATEADD(DAY,-10,GETDATE()), 'C-0001-00000015',NULL, 81000, 1),
(3, 4, 4, DATEADD(DAY,-11,GETDATE()), 'C-0001-00000016',NULL, 27000, 0),
(3, 5, 5, DATEADD(DAY,-12,GETDATE()), 'C-0001-00000017',NULL, 33600, 1),
(3, 6, 4, DATEADD(DAY,-13,GETDATE()), 'C-0001-00000018',NULL, 36600, 1),
(3, 1, 4, DATEADD(DAY,-14,GETDATE()), 'C-0001-00000019',NULL, 114000, 1),
(3, 2, 4, DATEADD(DAY,-15,GETDATE()), 'C-0001-00000020',NULL, 21600, 1),
(3, 3, 1, DATEADD(DAY,-16,GETDATE()), 'C-0001-00000021',NULL, 97200, 1),
(3, 4, 4, DATEADD(DAY,-17,GETDATE()), 'C-0001-00000022',NULL, 18000, 0),
(3, 5, 5, DATEADD(DAY,-18,GETDATE()), 'C-0001-00000023',NULL, 44800, 1),
(3, 6, 4, DATEADD(DAY,-19,GETDATE()), 'C-0001-00000024',NULL, 61000, 1),
(3, 1, 4, DATEADD(DAY,-20,GETDATE()), 'C-0001-00000025',NULL, 57000, 0);
GO

/* DETALLES COMPRA */
INSERT INTO DetallesCompra (IdCompra, IdArticulo, Cantidad, PrecioUnitario, SubTotal) VALUES
(1, 1, 4, 28500, 114000),
(2, 2, 2, 68500, 137000),
(3, 4, 3, 16200, 48600),
(4, 9, 7, 4500, 31500),
(5, 11, 4, 5600, 22400),
(6, 10, 5, 6100, 30500),
(7, 1, 5, 28500, 142500),
(8, 3, 4, 7200, 28800),
(9, 4, 4, 16200, 64800),
(10, 9, 10, 4500, 45000),
(11, 11, 5, 5600, 28000),
(12, 10, 4, 6100, 24400),
(13, 1, 3, 28500, 85500),
(14, 3, 6, 7200, 43200),
(15, 4, 5, 16200, 81000),
(16, 9, 6, 4500, 27000),
(17, 11, 6, 5600, 33600),
(18, 10, 6, 6100, 36600),
(19, 1, 4, 28500, 114000),
(20, 3, 3, 7200, 21600),
(21, 4, 6, 16200, 97200),
(22, 9, 4, 4500, 18000),
(23, 11, 8, 5600, 44800),
(24, 10, 10, 6100, 61000),
(25, 1, 2, 28500, 57000);
GO


/* ARTICULOSPROVEEDOR */

INSERT INTO ArticulosProveedor (IdProveedor, IdArticulo, Activo) VALUES
-- Distribuidora Patitas SA
(1, 1, 1),
(1, 4, 1),
(1, 6, 1),

-- Mundo Mascota SRL
(2, 2, 1),
(2, 7, 1),
(2, 8, 1),

-- Alimentos Caninos Norte
(3, 1, 1),
(3, 2, 1),
(3, 4, 1),
(3, 6, 1),

-- Pet Clean Mayorista
(4, 9, 1),
(4, 13, 1),

-- Accesorios Huellitas
(5, 7, 1),
(5, 8, 1),
(5, 11, 1),
(5, 12, 1),
(5, 14, 1),
(5, 15, 0),

-- Sanitarios Felinos SRL
(6, 10, 1),

-- Nutrican Mayorista (inactivo)
(7, 1, 0),
(7, 3, 0),
(7, 5, 0),

-- ZooMarket Distribuciones (inactivo)
(8, 7, 0),
(8, 11, 0),
(8, 12, 0);
GO
