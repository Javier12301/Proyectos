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


