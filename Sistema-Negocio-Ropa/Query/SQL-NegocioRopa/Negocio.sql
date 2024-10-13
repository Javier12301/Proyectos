USE TiendaDeRopa
GO

-- NO OLVIDAR DE DISEÑAR CÓDIGO PARA DETECTAR REFERENCIAS
CREATE TABLE Categoria (
    CategoriaID INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE,
    Estado BIT DEFAULT 1
);
-- Insertar la categoría 'N/A' con el estado desactivado (0)
SET IDENTITY_INSERT Categoria ON;
INSERT INTO Categoria (CategoriaID, Nombre, Estado) VALUES (0, 'Sin categoría', 1);
SET IDENTITY_INSERT Categoria OFF;
-- PRODUCTO
-- PRODUCTO con StockMinimo
CREATE TABLE Producto (
    ProductoID INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    CategoriaID INT REFERENCES Categoria(CategoriaID) ON DELETE SET NULL,
    PrecioVenta DECIMAL(10,2) NOT NULL,
    Talle NVARCHAR(50),
    Equipo NVARCHAR(50),
    Stock INT DEFAULT 0,
    StockMinimo INT DEFAULT 0, -- nuevo campo
    Estado BIT DEFAULT 1
);

-- COMPRA
CREATE TABLE Compra (
    CompraID INT IDENTITY PRIMARY KEY,
    UsuarioID INT REFERENCES Usuario(UsuarioID) ON DELETE SET NULL,
    Factura NVARCHAR(20),
    FechaCompra DATETIME DEFAULT GETDATE(),
    Estado BIT DEFAULT 1
);

-- DETALLE_COMPRA con SubTotal calculado
CREATE TABLE Detalle_Compra (
    DetalleCompraID INT IDENTITY PRIMARY KEY,
    CompraID INT REFERENCES Compra(CompraID) ON DELETE CASCADE,
    ProductoID INT REFERENCES Producto(ProductoID),
    PrecioCompra DECIMAL(10,2) DEFAULT 0,
    Cantidad INT NOT NULL,
    SubTotal DECIMAL(10,2) -- nuevo campo
);


-- METODO_PAGO
CREATE TABLE MetodoPago (
    MetodoPagoID INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Recargo DECIMAL(10,2) DEFAULT 0
);


-- VENTA
CREATE TABLE Venta (
    VentaID INT IDENTITY PRIMARY KEY,
    UsuarioID INT REFERENCES Usuario(UsuarioID) ON DELETE SET NULL,
	CajaID INT REFERENCES Caja(CajaID) ON DELETE SET NULL,
    TipoComprobante NVARCHAR(50),
    MontoPagado DECIMAL(10,2),
    MontoCambio DECIMAL(10,2),
    Recargo DECIMAL(10,2),
    MetodoPagoID INT REFERENCES MetodoPago(MetodoPagoID),
    MontoTotal DECIMAL(10,2) NOT NULL,
    FechaVenta DATETIME DEFAULT GETDATE(),
    Estado BIT DEFAULT 1
);

-- DETALLE_VENTA
CREATE TABLE Detalle_Venta (
    DetalleVentaID INT IDENTITY PRIMARY KEY,
    VentaID INT REFERENCES Venta(VentaID) ON DELETE CASCADE,
    ProductoID INT REFERENCES Producto(ProductoID),
    PrecioVenta DECIMAL(10,2),
    Cantidad INT NOT NULL,
    SubTotal DECIMAL(10,2)
);

CREATE TABLE Caja (
    CajaID INT IDENTITY PRIMARY KEY,
    UsuarioID INT REFERENCES Usuario(UsuarioID),
    MontoInicial DECIMAL(10,2) NOT NULL,
    MontoFinal DECIMAL(10,2),
    FechaApertura DATETIME DEFAULT GETDATE(),
    FechaCierre DATETIME,
    Estado BIT DEFAULT 1,
);

-- NEGOCIO
CREATE TABLE Negocio (
    NegocioID INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(60),
    Direccion NVARCHAR(60),
    Ciudad NVARCHAR(60),
    CodigoPostal NVARCHAR(20),
    NombreCompletoPropietario NVARCHAR(60),
    Telefono NVARCHAR(60),
    TipoDocumento NVARCHAR(60) DEFAULT 'CUIT',
    Documento NVARCHAR(60),
);



SELECT * FROM MetodoPago

