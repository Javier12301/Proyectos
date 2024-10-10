USE TiendaDeRopa;
GO

-- 1. Eliminar la tabla Detalle_Venta
IF OBJECT_ID('Detalle_Venta', 'U') IS NOT NULL
BEGIN
    DROP TABLE Detalle_Venta;
END

-- 2. Eliminar la tabla Venta
IF OBJECT_ID('Venta', 'U') IS NOT NULL
BEGIN
    DROP TABLE Venta;
END

-- 3. Eliminar la tabla Detalle_Compra
IF OBJECT_ID('Detalle_Compra', 'U') IS NOT NULL
BEGIN
    DROP TABLE Detalle_Compra;
END

-- 4. Eliminar la tabla Compra
IF OBJECT_ID('Compra', 'U') IS NOT NULL
BEGIN
    DROP TABLE Compra;
END

-- 5. Eliminar la tabla Producto
IF OBJECT_ID('Producto', 'U') IS NOT NULL
BEGIN
    DROP TABLE Producto;
END

-- 6. Eliminar la tabla Categoria
-- (Elimina toda la tabla, ya no es necesario hacer la excepción para CategoriaID = 0)
IF OBJECT_ID('Categoria', 'U') IS NOT NULL
BEGIN
    DROP TABLE Categoria;
END

-- 7. Eliminar la tabla MetodoPago
IF OBJECT_ID('MetodoPago', 'U') IS NOT NULL
BEGIN
    DROP TABLE MetodoPago;
END

-- 8. Eliminar la tabla Negocio
IF OBJECT_ID('Negocio', 'U') IS NOT NULL
BEGIN
    DROP TABLE Negocio;
END
