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

EXEC sp_ObtenerUsuarioPorNombre @nombreUsuario = 'Admin';


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

EXEC ObtenerCategoriasConProductos

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
ALTER PROCEDURE sp_ObtenerProductosConNuevoPrecio
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


