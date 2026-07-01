
CREATE PROCEDURE sp_CrearCabeceraVenta
    @IdCliente INT,
    @IdUsuario INT,
    @IdMedioPago INT,
    @NumeroFactura VARCHAR(30),
    @Total DECIMAL(18,2)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Ventas WHERE NumeroFactura = @NumeroFactura AND Activo = 1)
    BEGIN
        RAISERROR('El número de factura ya existe en el sistema.', 16, 1);
        RETURN;
    END

    INSERT INTO Ventas (IdCliente, IdUsuario, IdMedioPago, Fecha, NumeroFactura, Total, Activo)
    VALUES (@IdCliente, @IdUsuario, @IdMedioPago, GETDATE(), @NumeroFactura, @Total, 1);

    SELECT SCOPE_IDENTITY() AS IdVenta;
END;
GO

CREATE PROCEDURE sp_AgregarDetalleVenta
    @IdVenta INT,
    @IdArticulo INT,
    @Cantidad INT
AS
BEGIN
    DECLARE @PrecioUnitario DECIMAL(18,2);
    SELECT @PrecioUnitario = PrecioUnitario FROM Articulos WHERE IdArticulo = @IdArticulo;

    INSERT INTO DetallesVenta (IdVenta, IdArticulo, Cantidad, PrecioUnitario, SubTotal)
    VALUES (@IdVenta, @IdArticulo, @Cantidad, @PrecioUnitario, @PrecioUnitario * @Cantidad);
END;
GO

CREATE PROCEDURE sp_EliminarVenta
    @IdVenta INT
AS
BEGIN
    UPDATE Ventas
    SET Activo = 0
    WHERE IdVenta = @IdVenta;
END;
GO

CREATE TRIGGER TR_ReponerStockVentaCancelada
ON Ventas
AFTER UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1 
        FROM inserted I
        INNER JOIN deleted D ON I.IdVenta = D.IdVenta
        WHERE D.Activo = 1 AND I.Activo = 0
    )
    BEGIN
        UPDATE A
        SET A.Stock = A.Stock + DV.Cantidad
        FROM Articulos A
        INNER JOIN DetallesVenta DV ON A.IdArticulo = DV.IdArticulo
        INNER JOIN inserted I ON DV.IdVenta = I.IdVenta
        INNER JOIN deleted D ON I.IdVenta = D.IdVenta
        WHERE D.Activo = 1 AND I.Activo = 0;
    END
END;
GO
