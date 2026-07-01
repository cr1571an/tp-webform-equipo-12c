IF DB_ID('COMERCIO_BBDD') IS NULL
BEGIN
    CREATE DATABASE COMERCIO_BBDD;
END
GO

USE COMERCIO_BBDD;
GO

CREATE TABLE Clientes (
    IdCliente INT IDENTITY(1,1) NOT NULL,
    IdCondicionIva INT NOT NULL,
    Cuit VARCHAR(13) NULL,
    Nombre VARCHAR(55) NOT NULL,
    Apellido VARCHAR(55) NOT NULL,
    Telefono VARCHAR(15) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Cp VARCHAR(6) NOT NULL,
    Domicilio VARCHAR(255) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Clientes PRIMARY KEY (IdCliente),
    CONSTRAINT UQ_Clientes_Email UNIQUE (Email)
);
GO

CREATE TABLE Empleados (
    IdEmpleado INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(55) NOT NULL,
    Apellido VARCHAR(55) NOT NULL,
    Telefono VARCHAR(15) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Dni VARCHAR(10) NOT NULL,
    FechaIngreso DATE NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Empleados PRIMARY KEY (IdEmpleado),
    CONSTRAINT UQ_Empleados_Email UNIQUE (Email),
    CONSTRAINT UQ_Empleados_Dni UNIQUE (Dni),
    CONSTRAINT CK_Empleados_Dni CHECK (LEN(LTRIM(RTRIM(Dni))) > 0)
);
GO

CREATE TABLE Roles (
    IdRol INT IDENTITY(1,1) NOT NULL,
    NombreRol VARCHAR(55) NOT NULL,
    Descripcion VARCHAR(255) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Roles PRIMARY KEY (IdRol),
    CONSTRAINT UQ_Roles_NombreRol UNIQUE (NombreRol)
);
GO

CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) NOT NULL,
    IdEmpleado INT NOT NULL,
    IdRol INT NOT NULL,
    NombreUsuario VARCHAR(55) NOT NULL,
    PasswordHash VARCHAR(55) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Usuarios PRIMARY KEY (IdUsuario),
    CONSTRAINT UQ_Usuarios_IdEmpleado UNIQUE (IdEmpleado),
    CONSTRAINT UQ_Usuarios_NombreUsuario UNIQUE (NombreUsuario),
    CONSTRAINT CK_Usuarios_PasswordHash CHECK (LEN(LTRIM(RTRIM(PasswordHash))) > 0)
);
GO

CREATE TABLE Proveedores (
    IdProveedor INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(55) NOT NULL,
    Cuit VARCHAR(13) NOT NULL,
    Telefono VARCHAR(15) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Domicilio VARCHAR(255) NOT NULL,
    Cp VARCHAR(6) NULL,
    PersonaContacto VARCHAR(100) NULL,
    Observaciones VARCHAR(255) NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Proveedores PRIMARY KEY (IdProveedor),
    CONSTRAINT UQ_Proveedores_Nombre UNIQUE (Nombre),
    CONSTRAINT UQ_Proveedores_Cuit UNIQUE (Cuit),
    CONSTRAINT UQ_Proveedores_Email UNIQUE (Email)
);
GO

CREATE TABLE Categorias (
    IdCategoria INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(55) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Categorias PRIMARY KEY (IdCategoria),
    CONSTRAINT UQ_Categorias_Nombre UNIQUE (Nombre)
);
GO

CREATE TABLE Marcas (
    IdMarca INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(55) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Marcas PRIMARY KEY (IdMarca),
    CONSTRAINT UQ_Marcas_Nombre UNIQUE (Nombre)
);
GO

CREATE TABLE AlicuotaIva (
    IdAlicuotaIva INT IDENTITY(1,1) NOT NULL,
    AlicuotaIva DECIMAL(5,2) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_AlicuotaIva PRIMARY KEY (IdAlicuotaIva),
    CONSTRAINT UQ_AlicuotaIva_AlicuotaIva UNIQUE (AlicuotaIva)
);
GO

CREATE TABLE CondicionIva (
    IdCondicionIva INT IDENTITY(1,1) NOT NULL,
    Condicion VARCHAR(50) NOT NULL,
    CONSTRAINT PK_CondicionIva PRIMARY KEY (IdCondicionIva),
    CONSTRAINT UQ_CondicionIva_Condicion UNIQUE (Condicion)
);
GO

CREATE TABLE MediosPago (
    IdMedioPago INT IDENTITY(1,1) NOT NULL,
    Nombre VARCHAR(55) NOT NULL,
    Descripcion VARCHAR(255) NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_MediosPago PRIMARY KEY (IdMedioPago),
    CONSTRAINT UQ_MediosPago_Nombre UNIQUE (Nombre)
);
GO

CREATE TABLE Articulos (
    IdArticulo INT IDENTITY(1,1) NOT NULL,
    IdCategoria INT NOT NULL,
    IdAlicuotaIva INT NOT NULL,
    IdMarca INT NOT NULL,
    Nombre VARCHAR(55) NOT NULL,
    Descripcion VARCHAR(255) NULL,
    PrecioUnitario MONEY NOT NULL,
    Stock SMALLINT NOT NULL,
    UrlImagen VARCHAR(255) NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Articulos PRIMARY KEY (IdArticulo),
    CONSTRAINT UQ_Articulos_Nombre UNIQUE (Nombre),
    CONSTRAINT CK_Articulos_PrecioUnitario CHECK (PrecioUnitario > 0),
    CONSTRAINT CK_Articulos_Stock CHECK (Stock >= 0)
);
GO

CREATE TABLE Ventas (
    IdVenta INT IDENTITY(1,1) NOT NULL,
    IdCliente INT NOT NULL,
    IdUsuario INT NOT NULL,
    IdMedioPago INT NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    NumeroFactura VARCHAR(30) NOT NULL,
    Total MONEY NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Ventas PRIMARY KEY (IdVenta),
    CONSTRAINT UQ_Ventas_NumeroFactura UNIQUE (NumeroFactura),
    CONSTRAINT CK_Ventas_NumeroFactura CHECK (LEN(LTRIM(RTRIM(NumeroFactura))) > 0),
    CONSTRAINT CK_Ventas_Total CHECK (Total > 0)
);
GO

CREATE TABLE DetallesVenta (
    IdDetalleVenta INT IDENTITY(1,1) NOT NULL,
    IdVenta INT NOT NULL,
    IdArticulo INT NOT NULL,
    Cantidad SMALLINT NOT NULL,
    PrecioUnitario MONEY NOT NULL,
    SubTotal MONEY NOT NULL,
    CONSTRAINT PK_DetallesVenta PRIMARY KEY (IdDetalleVenta),
    CONSTRAINT CK_DetallesVenta_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT CK_DetallesVenta_PrecioUnitario CHECK (PrecioUnitario > 0),
    CONSTRAINT CK_DetallesVenta_SubTotal CHECK (SubTotal > 0)
);
GO

CREATE TABLE Compras (
    IdCompra INT IDENTITY(1,1) NOT NULL,
    IdUsuario INT NOT NULL,
    IdProveedor INT NOT NULL,
    IdMedioPago INT NOT NULL,
    FechaCompra DATETIME NOT NULL DEFAULT GETDATE(),
    NumeroFactura VARCHAR(30) NOT NULL,
    Observaciones VARCHAR(255) NULL,
    Total MONEY NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Compras PRIMARY KEY (IdCompra),
    CONSTRAINT UQ_Compras_NumeroFactura UNIQUE (NumeroFactura),
    CONSTRAINT CK_Compras_NumeroFactura CHECK (LEN(LTRIM(RTRIM(NumeroFactura))) > 0),
    CONSTRAINT CK_Compras_Total CHECK (Total > 0)
);
GO

CREATE TABLE DetallesCompra (
    IdDetalleCompra INT IDENTITY(1,1) NOT NULL,
    IdCompra INT NOT NULL,
    IdArticulo INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario MONEY NOT NULL,
    SubTotal MONEY NOT NULL,
    CONSTRAINT PK_DetallesCompra PRIMARY KEY (IdDetalleCompra),
    CONSTRAINT CK_DetallesCompra_Cantidad CHECK (Cantidad > 0),
    CONSTRAINT CK_DetallesCompra_PrecioUnitario CHECK (PrecioUnitario > 0),
    CONSTRAINT CK_DetallesCompra_SubTotal CHECK (SubTotal > 0)
);
GO

CREATE TABLE [ArticulosProveedor] (
	[IdProveedor] INTEGER NOT NULL,
	[IdArticulo] INTEGER NOT NULL,
	[Activo] BIT NOT NULL DEFAULT 1,
	PRIMARY KEY([IdProveedor], [IdArticulo])
);
GO

ALTER TABLE Clientes
ADD CONSTRAINT FK_Clientes_IdCondicionIva_CondicionIva
FOREIGN KEY (IdCondicionIva) REFERENCES CondicionIva(IdCondicionIva);
GO

ALTER TABLE Usuarios
ADD CONSTRAINT FK_Usuarios_IdEmpleado_Empleados
FOREIGN KEY (IdEmpleado) REFERENCES Empleados(IdEmpleado);
GO

ALTER TABLE Usuarios
ADD CONSTRAINT FK_Usuarios_IdRol_Roles
FOREIGN KEY (IdRol) REFERENCES Roles(IdRol);
GO

ALTER TABLE Articulos
ADD CONSTRAINT FK_Articulos_IdCategoria_Categorias
FOREIGN KEY (IdCategoria) REFERENCES Categorias(IdCategoria);
GO

ALTER TABLE Articulos
ADD CONSTRAINT FK_Articulos_IdAlicuotaIva_AlicuotaIva
FOREIGN KEY (IdAlicuotaIva) REFERENCES AlicuotaIva(IdAlicuotaIva);
GO

ALTER TABLE Articulos
ADD CONSTRAINT FK_Articulos_IdMarca_Marcas
FOREIGN KEY (IdMarca) REFERENCES Marcas(IdMarca);
GO

ALTER TABLE Ventas
ADD CONSTRAINT FK_Ventas_IdCliente_Clientes
FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente);
GO

ALTER TABLE Ventas
ADD CONSTRAINT FK_Ventas_IdUsuario_Usuarios
FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario);
GO

ALTER TABLE Ventas
ADD CONSTRAINT FK_Ventas_IdMedioPago_MediosPago
FOREIGN KEY (IdMedioPago) REFERENCES MediosPago(IdMedioPago);
GO

ALTER TABLE DetallesVenta
ADD CONSTRAINT FK_DetallesVenta_IdVenta_Ventas
FOREIGN KEY (IdVenta) REFERENCES Ventas(IdVenta);
GO

ALTER TABLE DetallesVenta
ADD CONSTRAINT FK_DetallesVenta_IdArticulo_Articulos
FOREIGN KEY (IdArticulo) REFERENCES Articulos(IdArticulo);
GO

ALTER TABLE Compras
ADD CONSTRAINT FK_Compras_IdUsuario_Usuarios
FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario);
GO

ALTER TABLE Compras
ADD CONSTRAINT FK_Compras_IdProveedor_Proveedores
FOREIGN KEY (IdProveedor) REFERENCES Proveedores(IdProveedor);
GO

ALTER TABLE Compras
ADD CONSTRAINT FK_Compras_IdMedioPago_MediosPago
FOREIGN KEY (IdMedioPago) REFERENCES MediosPago(IdMedioPago);
GO

ALTER TABLE DetallesCompra
ADD CONSTRAINT FK_DetallesCompra_IdCompra_Compras
FOREIGN KEY (IdCompra) REFERENCES Compras(IdCompra);
GO

ALTER TABLE DetallesCompra
ADD CONSTRAINT FK_DetallesCompra_IdArticulo_Articulos
FOREIGN KEY (IdArticulo) REFERENCES Articulos(IdArticulo);
GO

ALTER TABLE [ArticulosProveedor]
ADD FOREIGN KEY([IdArticulo])
REFERENCES [Articulos]([IdArticulo])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO
ALTER TABLE [ArticulosProveedor]
ADD FOREIGN KEY([IdProveedor])
REFERENCES [Proveedores]([IdProveedor])
ON UPDATE NO ACTION ON DELETE NO ACTION;
GO