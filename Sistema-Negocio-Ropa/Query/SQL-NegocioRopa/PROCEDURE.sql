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


CREATE PROCEDURE sp_ObtenerProductosConNuevoPrecio
    @Buscar NVARCHAR(255),
    @FiltroCategoria NVARCHAR(50),
    @Porcentaje DECIMAL(5, 2) -- Porcentaje para ajustar el precio
AS
BEGIN
    SELECT 
        P.ProductoID AS ID,
        P.Nombre AS Producto,
        P.PrecioVenta AS Precio,
        -- Calcular el nuevo precio aplicando el porcentaje
        CASE 
            WHEN (P.PrecioVenta + (P.PrecioVenta * @Porcentaje / 100)) < 0 
            THEN 0 
            ELSE (P.PrecioVenta + (P.PrecioVenta * @Porcentaje / 100)) 
        END AS NuevoPrecio
    FROM 
        dbo.Producto P
        INNER JOIN dbo.Categoria C ON P.CategoriaID = C.CategoriaID
    WHERE
        (@FiltroCategoria = 'Todos' OR C.Nombre = @FiltroCategoria)
        AND (
            (P.Nombre LIKE '%' + @Buscar + '%') 
            OR (C.Nombre LIKE '%' + @Buscar + '%')
        )
        AND P.Estado = 1; -- Solo mostrar productos activos
END;


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


-- CAJA
ALTER PROCEDURE sp_ObtenerCajasConTotales
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


