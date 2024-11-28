-- Crear Grupo
CREATE TABLE Grupo (
    GrupoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1  -- Activo por defecto
);

SET IDENTITY_INSERT Grupo ON;
INSERT INTO Grupo (GrupoID, Nombre, Estado)
VALUES (0, 'N/A', 0);
SET IDENTITY_INSERT Grupo OFF;
GO

-- Crear Usuario
CREATE TABLE Usuario (
    UsuarioID INT IDENTITY(1,1) PRIMARY KEY,
    Usuario NVARCHAR(100) NOT NULL UNIQUE,
    Contrasena NVARCHAR(255) NOT NULL,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    Correo NVARCHAR(100) NOT NULL,
    GrupoID INT,
    Estado BIT NOT NULL DEFAULT 1,  -- Activo por defecto
    FOREIGN KEY (GrupoID) REFERENCES Grupo(GrupoID) ON DELETE SET NULL  -- Mantener integridad
);

-- Crear Modulo
CREATE TABLE Modulo (
    ModuloID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);

-- Crear Accion
CREATE TABLE Accion (
    AccionID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    ModuloID INT,
    FOREIGN KEY (ModuloID) REFERENCES Modulo(ModuloID) ON DELETE CASCADE  -- Borrar acciones si se elimina el módulo
);

-- Crear Permiso
CREATE TABLE Permiso (
    PermisoID INT IDENTITY(1,1) PRIMARY KEY,
    GrupoID INT NOT NULL,
    AccionID INT NOT NULL,
    Permitido BIT NOT NULL DEFAULT 0,  -- No permitido por defecto
    FOREIGN KEY (GrupoID) REFERENCES Grupo(GrupoID) ON DELETE CASCADE,  -- Borrar permisos si se elimina el grupo
    FOREIGN KEY (AccionID) REFERENCES Accion(AccionID) ON DELETE CASCADE  -- Borrar permisos si se elimina la acción
);

-- Crear Auditoria
CREATE TABLE Auditoria (
    AuditoriaID INT IDENTITY(1,1) PRIMARY KEY,
    FechayHora DATETIME DEFAULT GETDATE(),  -- Fecha y hora por defecto
    Movimiento NVARCHAR(255) NOT NULL,
    Modulo NVARCHAR(255),  -- Se puede enlazar a la tabla Modulo
    NombreUsuario NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255)
);
GO

-- Crear Categoria
CREATE TABLE Categoria (
    CategoriaID INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL UNIQUE,
    Estado BIT DEFAULT 1
);

SET IDENTITY_INSERT Categoria ON;
INSERT INTO Categoria (CategoriaID, Nombre, Estado) VALUES (0, 'Sin categoría', 1);
SET IDENTITY_INSERT Categoria OFF;

-- Crear Producto
CREATE TABLE Producto (
    ProductoID INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(max),
    CategoriaID INT REFERENCES Categoria(CategoriaID) ON DELETE SET NULL,
    PrecioVenta DECIMAL(10,2) NOT NULL,
    Talle NVARCHAR(50),
    Equipo NVARCHAR(50),
    Stock INT DEFAULT 0,
    StockMinimo INT DEFAULT 0, -- nuevo campo
    Estado BIT DEFAULT 1
);




-- Crear MetodoPago
CREATE TABLE MetodoPago (
    MetodoPagoID INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Recargo DECIMAL(10,2) DEFAULT 0
);

-- Crear Compra
CREATE TABLE Compra (
    CompraID INT IDENTITY PRIMARY KEY,
    UsuarioID INT REFERENCES Usuario(UsuarioID) ON DELETE SET NULL,
    Factura NVARCHAR(20),
    FechaCompra DATETIME DEFAULT GETDATE(),
    Estado BIT DEFAULT 1
);

-- Crear Detalle_Compra
CREATE TABLE Detalle_Compra (
    DetalleCompraID INT IDENTITY PRIMARY KEY,
    CompraID INT REFERENCES Compra(CompraID) ON DELETE CASCADE,
    ProductoID INT REFERENCES Producto(ProductoID),
    PrecioCompra DECIMAL(10,2) DEFAULT 0,
    Cantidad INT NOT NULL,
    SubTotal DECIMAL(10,2) -- nuevo campo
);

-- Crear Caja
CREATE TABLE Caja (
    CajaID INT IDENTITY PRIMARY KEY,
    UsuarioID INT REFERENCES Usuario(UsuarioID),
    MontoInicial DECIMAL(10,2) NOT NULL,
    MontoFinal DECIMAL(10,2),
    FechaApertura DATETIME DEFAULT GETDATE(),
    FechaCierre DATETIME,
    Estado BIT DEFAULT 1,
    Nota NVARCHAR(MAX)  -- Nueva columna para la nota
);

-- Crear Venta
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

-- Crear Detalle_Venta
CREATE TABLE Detalle_Venta (
    DetalleVentaID INT IDENTITY PRIMARY KEY,
    VentaID INT REFERENCES Venta(VentaID) ON DELETE CASCADE,
    ProductoID INT REFERENCES Producto(ProductoID),
    PrecioVenta DECIMAL(10,2),
    Cantidad INT NOT NULL,
    SubTotal DECIMAL(10,2)
);

-- Crear Negocio
CREATE TABLE Negocio (
    NegocioID INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(60),
    Direccion NVARCHAR(60),
    Ciudad NVARCHAR(60),
    CodigoPostal NVARCHAR(20),
    NombreCompletoPropietario NVARCHAR(60),
    Telefono NVARCHAR(60),
    TipoDocumento NVARCHAR(60) DEFAULT 'CUIT',
    Documento NVARCHAR(60)
);


---------- INSERTS -- DE SEGURIDAD -- -- -- --

-- Insertar el grupo "Administrador"
INSERT INTO Grupo (Nombre, Estado)
VALUES ('Administrador', 1);
GO

-- Insertar módulos
INSERT INTO Modulo(Nombre)
VALUES 
    ('formVentas'),
    ('formProductos'),
    ('formCategorias'),
    ('formInventario'),
    ('formReportes'),
    ('formAjustes'),
    ('formUsuarios'),
    ('formGrupos'),
    ('formAuditoria'),
    ('formMisDatos'),
	('formNegocio'),
	('formBackup');
GO

-- Insertar usuario "Admin"
INSERT INTO [dbo].[Usuario]
           ([Usuario], [Contrasena], [Nombre], [Apellido], [Correo], [GrupoID], [Estado])
     VALUES ('Admin', 'C1C224B03CD9BC7B6A86D77F5DACE40191766C485CD55DC48CAF9AC873335D6F','Admin', '-', 'javierramirez1230123@gmail.com', 1, 1);
GO

-- Insertar las acciones para el módulo formVentas
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Vender', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formVentas'));
GO

-- Insertar las acciones para el módulo formProductos
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Alta', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formProductos')),
    ('Modificar', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formProductos')),
    ('Baja', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formProductos')),
	('Exportar', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formProductos'));
GO

-- Insertar las acciones para el módulo formCategorias
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Alta', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formCategorias')),
    ('Modificar', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formCategorias')),
    ('Baja', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formCategorias'));
GO

-- Insertar las acciones para el módulo formInventario
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Comprar', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formInventario')),
	('Exportar', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formInventario'));
GO

-- Insertar las acciones para el módulo formReportes
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Permitir_Acceso', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formReportes'));
GO

-- Insertar las acciones para el módulo formAjustes
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Permitir_Acceso', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formAjustes'));
GO

-- Insertar las acciones para el módulo formUsuarios
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Alta', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formUsuarios')),
    ('Modificar', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formUsuarios')),
    ('Baja', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formUsuarios'));
GO

-- Insertar las acciones para el módulo formGrupos
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Alta', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formGrupos')),
    ('Modificar', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formGrupos')),
    ('Baja', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formGrupos'));
GO

-- Insertar las acciones para el módulo formAuditoria
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Permitir_Acceso', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formAuditoria'));
GO

-- Insertar las acciones para el módulo formMisDatos
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Permitir_Acceso', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formMisDatos'));
GO

-- Insertar las acciones para el módulo formBackup
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Permitir_Acceso', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formBackup'));
GO

-- Insertar las acciones para el módulo formNegocio
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Permitir_Acceso', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formNegocio'));
GO

-- Asignar permisos totales al grupo "Administrador" para todas las acciones
INSERT INTO Permiso (GrupoID, AccionID, Permitido)
SELECT 1, AccionID, 1
FROM Accion;
GO

INSERT INTO Negocio (Nombre, Direccion, Ciudad, CodigoPostal, NombreCompletoPropietario, Telefono, TipoDocumento, Documento)
VALUES ('Tienda de ropa', '-', '-', '-', '-', '3855219032', 'CUIT/CUIL', '22000402');
GO

-- METODOS
INSERT INTO MetodoPago (Nombre, Recargo) VALUES ('Efectivo', 0);
INSERT INTO MetodoPago (Nombre, Recargo) VALUES ('Tarjeta Débito', 0);
INSERT INTO MetodoPago (Nombre, Recargo) VALUES ('Tarjeta Crédito', 0);
INSERT INTO MetodoPago (Nombre, Recargo) VALUES ('Depósito/Transferencia', 0);
GO

-- PROCEDIMIENTOS

CREATE PROCEDURE InsertarAuditoria
    @Movimiento NVARCHAR(255),
    @Modulo NVARCHAR(255),
    @NombreUsuario NVARCHAR(100),
    @Descripcion NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Auditoria (Movimiento, Modulo, NombreUsuario, Descripcion)
    VALUES (@Movimiento, @Modulo, @NombreUsuario, @Descripcion);
END;
GO

-- PROCEDIMIENTO DE OBTENCIÓN DE USUARIO POR NOMBRE
CREATE PROCEDURE sp_ObtenerUsuarioPorNombre
    @nombreUsuario NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT u.UsuarioID, u.Usuario, u.Contrasena , u.Nombre, u.Apellido, u.Correo, u.Estado, 
           u.GrupoID,          -- Se agrega GrupoID aquí
           g.Nombre AS Gr_Nombre, g.Estado AS Gr_Estado
    FROM Usuario u
    INNER JOIN Grupo g ON u.GrupoID = g.GrupoID
    WHERE u.Usuario = @nombreUsuario;
END;
GO

CREATE PROCEDURE sp_ObtenerUsuarioPorID
    @usuarioID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT u.UsuarioID, u.Usuario, u.Contrasena, u.Nombre, u.Apellido, u.Correo, u.Estado, 
           u.GrupoID,           -- Se agrega GrupoID aquí
           g.Nombre AS Gr_Nombre, g.Estado AS Gr_Estado
    FROM Usuario u
    INNER JOIN Grupo g ON u.GrupoID = g.GrupoID
    WHERE u.UsuarioID = @usuarioID;
END;
GO



-- MODULOS PERMITIDOS / PERMISOS ACTIVADOS PARA EL GRUPO CORRESPONDIENTE -> A LA ID INGRESADA
CREATE PROCEDURE sp_ObtenerModulosPermitidos
    @GrupoID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT M.Nombre
    FROM Permiso p
    JOIN Grupo Gr ON Gr.GrupoID = p.GrupoID
    JOIN Accion op ON op.AccionID = p.AccionID
    JOIN Modulo M ON M.ModuloID = op.ModuloID
    WHERE Gr.GrupoID = @GrupoID AND p.Permitido = 1;
END;
GO


-- OBTENER TODA LAS ACCIONES PERMITIDAS
CREATE PROCEDURE sp_ObtenerAccionesPermitidasPorModulo
    @GrupoID INT,
    @moduloDescripcion NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT op.*
	FROM Permiso p
	JOIN Grupo Gr ON Gr.GrupoID = p.GrupoID
	JOIN Accion op ON op.AccionID = p.AccionID
	JOIN Modulo M ON M.ModuloID = op.ModuloID
	WHERE Gr.GrupoID = @GrupoID AND M.Nombre = @moduloDescripcion AND p.Permitido = 1;
END;
GO


-- FILTRO DINAMICOS - USUARIOS
CREATE PROCEDURE sp_ObtenerUsuariosConFiltros
    @FiltroBuscar NVARCHAR(50),    -- 'Todos', 'Usuario', 'Nombre', 'Apellido', 'Email'
    @Buscar NVARCHAR(100),         -- Texto de búsqueda, puede estar vacío ('')
    @FiltroGrupo NVARCHAR(50),     -- 'Todos' o el nombre específico de un grupo
    @FiltroEstado NVARCHAR(10)     -- 'Todos', 'Activo', 'Inactivo'
AS
BEGIN
    -- Configuración para evitar mensajes innecesarios
    SET NOCOUNT ON;

    -- Consulta con filtros dinámicos
    SELECT 
        Usuario.UsuarioID AS ID,     -- ID del usuario
        Usuario.Usuario,              -- Usuario
        Usuario.Nombre,               -- Nombre
        Usuario.Apellido,             -- Apellido
        Usuario.Correo,               -- Correo
        Grupo.Nombre AS Grupo,        -- Nombre del Grupo
        Usuario.Estado                -- Estado
    FROM 
        Usuario
    INNER JOIN 
        Grupo ON Usuario.GrupoID = Grupo.GrupoID
    WHERE
        (
            (@FiltroBuscar = 'Todos' AND (
                Usuario.Usuario LIKE '%' + @Buscar + '%' OR
                Usuario.Nombre LIKE '%' + @Buscar + '%' OR
                Usuario.Apellido LIKE '%' + @Buscar + '%' OR
                Usuario.Correo LIKE '%' + @Buscar + '%'
            ))
            OR (@FiltroBuscar = 'Usuario' AND Usuario.Usuario LIKE '%' + @Buscar + '%')
            OR (@FiltroBuscar = 'Nombre' AND Usuario.Nombre LIKE '%' + @Buscar + '%')
            OR (@FiltroBuscar = 'Apellido' AND Usuario.Apellido LIKE '%' + @Buscar + '%')
            OR (@FiltroBuscar = 'Email' AND Usuario.Correo LIKE '%' + @Buscar + '%')
        )
        AND (@FiltroGrupo = 'Todos' OR Grupo.Nombre = @FiltroGrupo)
        AND (
            @FiltroEstado = 'Todos' OR 
            (Usuario.Estado = 1 AND @FiltroEstado = 'Activo') OR 
            (Usuario.Estado = 0 AND @FiltroEstado = 'Inactivo')
        );
END;
GO


-- FILTRO DINAMICOS - GRUPO
CREATE PROCEDURE sp_ObtenerGruposConFiltros
    @FiltroBuscar NVARCHAR(50),    -- 'Todos', 'Nombre'
    @Buscar NVARCHAR(100),         -- Texto de búsqueda, puede estar vacío ('')
    @FiltroEstado NVARCHAR(10)     -- 'Todos', 'Activo', 'Inactivo'
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        GrupoID AS ID,      -- Alias para cambiar GrupoID por ID
        Nombre,             -- Ya está con el nombre que deseas
        Estado
    FROM Grupo
    WHERE GrupoID > 0
      AND (
          (@FiltroBuscar = 'Todos' AND Nombre LIKE '%' + @Buscar + '%')
          OR
          (@FiltroBuscar = 'Nombre' AND Nombre LIKE '%' + @Buscar + '%')
      )
      AND (
          (@FiltroEstado = 'Todos')
          OR
          (@FiltroEstado = 'Activo' AND Estado = 1)
          OR
          (@FiltroEstado = 'Inactivo' AND Estado = 0)
      );
END;
GO


-- Filtro de Categorías
CREATE PROCEDURE FiltrarCategorias
    @FiltroEstado NVARCHAR(10),  -- Puede ser 'Activo', 'Inactivo' o 'Todos'
    @Buscar NVARCHAR(100) = ''   -- Cadena de búsqueda, por defecto es vacía
AS
BEGIN
    -- Consulta para filtrar las categorías
    SELECT 
        CategoriaID AS ID,  -- Alias para mostrar CategoriaID como ID
        Nombre,
        Estado
    FROM 
        Categoria
    WHERE 
        CategoriaID > 0  -- Excluyendo la categoría 'N/A' con CategoriaID = 0
        AND (
            @FiltroEstado = 'Todos'  -- Si el filtro es 'Todos', muestra todas las categorías
            OR (Estado = 1 AND @FiltroEstado = 'Activo')  -- Solo categorías activas
            OR (Estado = 0 AND @FiltroEstado = 'Inactivo')  -- Solo categorías inactivas
        )
        AND (
            @Buscar = ''  -- Si @Buscar es una cadena vacía, no se aplica filtro
            OR Nombre LIKE '%' + @Buscar + '%'
        );
END;
GO

-- Filtrar Productos
CREATE PROCEDURE sp_ObtenerProductos
    @Buscar NVARCHAR(255),
    @FiltroBuscar NVARCHAR(50),
    @FiltroTalle NVARCHAR(50),
    @FiltroEquipo NVARCHAR(50),
    @FiltroEstado NVARCHAR(50),
    @FiltroCategoria NVARCHAR(50)
AS
BEGIN
    SELECT 
        P.ProductoID AS ID,
        P.Nombre AS Producto,
        C.Nombre AS Categoria,
        COALESCE(P.Talle, 'Sin talle') AS Talle,
        COALESCE(P.Equipo, 'Sin equipo') AS Equipo,
        P.PrecioVenta AS Precio,
        P.Stock,
        P.StockMinimo AS [Cant. Minima],
        P.Estado 
    FROM 
        dbo.Producto P
        INNER JOIN dbo.Categoria C ON P.CategoriaID = C.CategoriaID
    WHERE
        (
            (@FiltroBuscar = 'Todos' AND (
                P.Nombre LIKE '%' + @Buscar + '%' OR
                C.Nombre LIKE '%' + @Buscar + '%' OR
                P.Equipo LIKE '%' + @Buscar + '%'
            ))
            OR (@FiltroBuscar = 'Nombre' AND P.Nombre LIKE '%' + @Buscar + '%')
            OR (@FiltroBuscar = 'Categoría' AND C.Nombre LIKE '%' + @Buscar + '%')
            OR (@FiltroBuscar = 'Equipo' AND P.Equipo LIKE '%' + @Buscar + '%')
        )
        AND (@FiltroTalle = 'Todos' OR COALESCE(P.Talle, 'Sin talle') = @FiltroTalle)
        AND (@FiltroEquipo = 'Todos' OR COALESCE(P.Equipo, 'Sin equipo') = @FiltroEquipo)
        AND (@FiltroCategoria = 'Todos' OR C.Nombre = @FiltroCategoria)
        AND (
            @FiltroEstado = 'Todos' OR 
            (P.Estado = 1 AND @FiltroEstado = 'Activo') OR 
            (P.Estado = 0 AND @FiltroEstado = 'Inactivo')
        );
END;
GO


-- DETECTAR REFERENCIAS DE PRODCUTOS
CREATE PROCEDURE VerificarReferenciasProducto
    @ProductoID INT,
    @ReferenciasEncontradas BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializamos el valor de salida
    SET @ReferenciasEncontradas = 0;

    -- Verificar si el producto tiene referencias en la tabla Detalle_Venta
    IF EXISTS (SELECT 1 FROM Detalle_Venta WHERE ProductoID = @ProductoID)
    BEGIN
        SET @ReferenciasEncontradas = 1;
        RETURN;
    END

    -- Verificar si el producto tiene referencias en la tabla Detalle_Compra
    IF EXISTS (SELECT 1 FROM Detalle_Compra WHERE ProductoID = @ProductoID)
    BEGIN
        SET @ReferenciasEncontradas = 1;
        RETURN;
    END
END;
GO

CREATE PROCEDURE VerificarReferenciasCategoria
    @CategoriaID INT,
    @ReferenciasEncontradas BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializamos el valor de salida
    SET @ReferenciasEncontradas = 0;

    -- Verificar si la categoría tiene productos asociados
    IF EXISTS (SELECT 1 FROM Producto WHERE CategoriaID = @CategoriaID)
    BEGIN
        SET @ReferenciasEncontradas = 1;
        RETURN;
    END
END;
GO

CREATE PROCEDURE VerificarReferenciasUsuario
    @UsuarioID INT,
    @ReferenciasEncontradas BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializamos el valor de salida
    SET @ReferenciasEncontradas = 0;

    -- Verificar si el usuario tiene referencias en la tabla Venta
    IF EXISTS (SELECT 1 FROM Venta WHERE UsuarioID = @UsuarioID)
    BEGIN
        SET @ReferenciasEncontradas = 1;
        RETURN;
    END

    -- Verificar si el usuario tiene referencias en la tabla Compra
    IF EXISTS (SELECT 1 FROM Compra WHERE UsuarioID = @UsuarioID)
    BEGIN
        SET @ReferenciasEncontradas = 1;
        RETURN;
    END
END;
GO

CREATE PROCEDURE VerificarReferenciasGrupo
    @GrupoID INT,
    @ReferenciasEncontradas BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Inicializamos el valor de salida
    SET @ReferenciasEncontradas = 0;

    -- Verificar si el grupo tiene usuarios asignados
    IF EXISTS (SELECT 1 FROM Usuario WHERE GrupoID = @GrupoID)
    BEGIN
        SET @ReferenciasEncontradas = 1;
        RETURN;
    END
END;
GO

-- LISTAR CATEGORÍAS CON PRODUCTOS REFERENCIADOS
CREATE PROCEDURE ObtenerCategoriasConProductos
AS
BEGIN
    SET NOCOUNT ON; -- Evita que se muestren contadores de filas afectadas

    SELECT DISTINCT c.CategoriaID, c.Nombre
    FROM Categoria c
    INNER JOIN Producto p ON c.CategoriaID = p.CategoriaID
    WHERE c.Estado = 1 AND c.CategoriaID > 0; -- Filtra solo categorías activas
END
GO

-- PROCEDIMIENTO PARA AJUSTAR PRECIO POR CATEGORIA
CREATE PROCEDURE sp_ObtenerProductosConNuevoPrecio
    @Buscar NVARCHAR(255),
    @FiltroCategoria NVARCHAR(50),
    @Porcentaje DECIMAL(5, 2)
AS
BEGIN
    SELECT 
        P.ProductoID AS ID,
        P.Nombre AS Producto,
        COALESCE(P.Talle, 'Sin talle') AS Talle,
        COALESCE(P.Equipo, 'Sin equipo') AS Equipo,
        P.PrecioVenta AS Precio,
        CAST(
            CASE
                WHEN P.PrecioVenta * (1 + (@Porcentaje / 100)) < 0 THEN 0
                ELSE P.PrecioVenta * (1 + (@Porcentaje / 100))
            END AS DECIMAL(10, 2)) AS [Nuevo Precio]
    FROM 
        dbo.Producto P
        INNER JOIN dbo.Categoria C ON P.CategoriaID = C.CategoriaID
    WHERE 
        C.Nombre = @FiltroCategoria
END;
GO


-- COMPRAR REALIZADAS
CREATE PROCEDURE sp_ObtenerComprasConTotales
    @FiltroEstado NVARCHAR(50),
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SELECT 
        C.CompraID AS Folio, 
        C.Factura AS Documento, 
        C.FechaCompra AS 'Fecha de compra', 
        SUM(DC.Cantidad) AS 'Cantidad de productos', 
        SUM(DC.Cantidad * DC.PrecioCompra) AS 'Precio total'
    FROM 
        Compra C
        INNER JOIN Detalle_Compra DC ON C.CompraID = DC.CompraID
    WHERE 
        (
            @FiltroEstado = 'Todos' OR 
            (C.Estado = 1 AND @FiltroEstado = 'Activo') OR 
            (C.Estado = 0 AND @FiltroEstado = 'Cancelado')
        )
        AND C.FechaCompra BETWEEN @FechaInicio AND @FechaFin
    GROUP BY 
        C.CompraID, 
        C.Factura,
        C.FechaCompra;
END;
GO

-- DETALLE DE COMPRA
CREATE PROCEDURE sp_ObtenerDetalleCompra
    @CompraID INT
AS
BEGIN
    SELECT 
		P.ProductoID AS 'ID',
        P.Nombre AS 'Nombre de Producto',
        DC.Cantidad AS 'Cantidad Comprada',
        DC.PrecioCompra AS 'Precio de Compra',
        DC.Cantidad * DC.PrecioCompra AS 'Sub Total'
    FROM 
        Detalle_Compra DC
        INNER JOIN Producto P ON DC.ProductoID = P.ProductoID
    WHERE 
        DC.CompraID = @CompraID;
END;
GO


-- CAJA
CREATE PROCEDURE sp_ObtenerCajasConTotales
    @FiltroEstado NVARCHAR(50),
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    -- Selección de cajas con totales por método de pago y cálculo de diferencia
    SELECT 
        C.CajaID,
        U.Usuario AS 'Usuario Responsable',                     -- Usuario que abrió la caja
        C.FechaApertura AS 'Fecha de Apertura',               -- Fecha en que se abrió la caja
        C.FechaCierre AS 'Fecha de Cierre',                   -- Fecha en que se cerró la caja
        
        -- Monto Inicial de la caja al abrir
        C.MontoInicial AS 'Monto Inicial',
        
        -- Totales por método de pago
        ISNULL(SUM(CASE WHEN MP.Nombre = 'Efectivo' THEN V.MontoTotal ELSE 0 END), 0) AS 'Total Efectivo',
        ISNULL(SUM(CASE WHEN MP.Nombre = 'Tarjeta Débito' THEN V.MontoTotal ELSE 0 END), 0) AS 'Total Débito',
        ISNULL(SUM(CASE WHEN MP.Nombre = 'Tarjeta Crédito' THEN V.MontoTotal ELSE 0 END), 0) AS 'Total Crédito',
        ISNULL(SUM(CASE WHEN MP.Nombre = 'Depósito/Transferencia' THEN V.MontoTotal ELSE 0 END), 0) AS 'Total Transferencia',
        
        -- Total de todas las ventas realizadas
        ISNULL(SUM(V.MontoTotal), 0) AS 'Total Ventas',
        
        -- Monto Final en caja (efectivo)
        ISNULL(C.MontoFinal, 0) AS 'Monto Final en Caja',
        
        -- Cálculo de la diferencia entre el Monto Final y el Monto Inicial + Total Efectivo
        ISNULL(C.MontoFinal, 0) - (C.MontoInicial + ISNULL(SUM(CASE WHEN MP.Nombre = 'Efectivo' THEN V.MontoTotal ELSE 0 END), 0)) AS 'Diferencia'
        
    FROM 
        Caja C
        INNER JOIN Usuario U ON C.UsuarioID = U.UsuarioID                         -- Unir con usuario que abrió la caja
        LEFT JOIN Venta V ON C.CajaID = V.CajaID AND V.Estado = 1               -- Solo considerar ventas activas
        LEFT JOIN MetodoPago MP ON V.MetodoPagoID = MP.MetodoPagoID              -- Unir con métodos de pago
    WHERE 
        (
            @FiltroEstado = 'Todos' OR                                            -- Filtrar por estado
            (C.Estado = 1 AND @FiltroEstado = 'Abierta') OR 
            (C.Estado = 0 AND @FiltroEstado = 'Cerrada')
        )
        AND C.FechaApertura BETWEEN @FechaInicio AND @FechaFin                   -- Filtrar por rango de fechas
    GROUP BY 
        C.CajaID, 
        U.Usuario, 
        C.MontoInicial, 
        C.MontoFinal, 
        C.FechaApertura, 
        C.FechaCierre;
END;
GO

CREATE PROCEDURE sp_ObtenerVentasConTotales
    @CajaFiltro NVARCHAR(50), -- 'Todas' o número de Caja
    @FiltroEstado NVARCHAR(50), -- 'Todos', 'Activo' o 'Cancelado'
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SELECT 
        V.VentaID AS Folio,
        U.Usuario AS 'Vendedor',
        MP.Nombre AS 'Método de Pago',
        SUM(DV.Cantidad) AS 'Cantidad Vendida',
        V.MontoPagado AS 'Monto Pagado',
        V.MontoCambio AS 'Monto Cambio',
        V.Recargo AS 'Recargo Aplicado',
        V.MontoTotal AS 'Monto Total'
    FROM 
        Venta V
        INNER JOIN Usuario U ON V.UsuarioID = U.UsuarioID
        INNER JOIN MetodoPago MP ON V.MetodoPagoID = MP.MetodoPagoID
        INNER JOIN Detalle_Venta DV ON V.VentaID = DV.VentaID
    WHERE 
        (@CajaFiltro = 'Todas' OR V.CajaID = @CajaFiltro) -- Filtro por Caja
        AND (
            @FiltroEstado = 'Todos' OR 
            (@FiltroEstado = 'Activo' AND V.Estado = 1) OR 
            (@FiltroEstado = 'Cancelado' AND V.Estado = 0)
        ) -- Filtro por Estado
        AND V.FechaVenta BETWEEN @FechaInicio AND @FechaFin -- Filtro por Fecha
    GROUP BY 
        V.VentaID, 
        U.Usuario, 
        MP.Nombre, 
        V.MontoPagado, 
        V.MontoCambio, 
        V.Recargo, 
        V.MontoTotal;
END;
GO

CREATE PROCEDURE sp_ObtenerDetalleVenta
    @VentaID INT
AS
BEGIN
    SELECT 
        P.ProductoID AS 'ID',
        P.Nombre AS 'Nombre de Producto',
        DV.Cantidad AS 'Cantidad Vendida',
        DV.PrecioVenta AS 'Precio de Venta',
        DV.Cantidad * DV.PrecioVenta AS 'Sub Total'
    FROM 
        Detalle_Venta DV
        INNER JOIN Producto P ON DV.ProductoID = P.ProductoID
    WHERE 
        DV.VentaID = @VentaID;
END;
GO



CREATE PROCEDURE sp_ObtenerTotalesPorMetodoPago
    @CajaID INT
AS
BEGIN
    SELECT 
        SUM(CASE WHEN MP.Nombre = 'Efectivo' THEN V.MontoTotal ELSE 0 END) AS 'Total Efectivo',
        SUM(CASE WHEN MP.Nombre = 'Tarjeta Débito' THEN V.MontoTotal ELSE 0 END) AS 'Total Débito',
        SUM(CASE WHEN MP.Nombre = 'Tarjeta Crédito' THEN V.MontoTotal ELSE 0 END) AS 'Total Crédito',
        SUM(CASE WHEN MP.Nombre = 'Depósito/Transferencia' THEN V.MontoTotal ELSE 0 END) AS 'Total Transferencia'
    FROM 
        Venta V
        LEFT JOIN MetodoPago MP ON V.MetodoPagoID = MP.MetodoPagoID
    WHERE 
        V.CajaID = @CajaID AND V.Estado = 1; -- Solo considerar ventas activas
END;
GO



