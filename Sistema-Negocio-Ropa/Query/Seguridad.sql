-- Tabla para Grupos de usuarios
CREATE TABLE Grupo (
    GrupoID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1  -- Activo por defecto
);

-- Estante default
SET IDENTITY_INSERT Grupo ON;
INSERT INTO Grupo (GrupoID, Nombre, Estado)
VALUES (0, 'N/A', 0);
SET IDENTITY_INSERT Grupo OFF;
GO

-- Tabla para Usuarios
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

-- Tabla para Módulos
CREATE TABLE Modulo (
    ModuloID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL
);

-- Tabla para Acciones
CREATE TABLE Accion (
    AccionID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    ModuloID INT,
    FOREIGN KEY (ModuloID) REFERENCES Modulo(ModuloID) ON DELETE CASCADE  -- Borrar acciones si se elimina el módulo
);

-- Tabla para Permisos
CREATE TABLE Permiso (
    PermisoID INT IDENTITY(1,1) PRIMARY KEY,
    GrupoID INT NOT NULL,
    AccionID INT NOT NULL,
    Permitido BIT NOT NULL DEFAULT 0,  -- No permitido por defecto
    FOREIGN KEY (GrupoID) REFERENCES Grupo(GrupoID) ON DELETE CASCADE,  -- Borrar permisos si se elimina el grupo
    FOREIGN KEY (AccionID) REFERENCES Accion(AccionID) ON DELETE CASCADE  -- Borrar permisos si se elimina la acción
);

-- Tabla para Auditoría
CREATE TABLE Auditoria (
    AuditoriaID INT IDENTITY(1,1) PRIMARY KEY,
    FechayHora DATETIME DEFAULT GETDATE(),  -- Fecha y hora por defecto
    Movimiento NVARCHAR(255) NOT NULL,
    Modulo NVARCHAR(255),  -- Se puede enlazar a la tabla Modulo
    NombreUsuario NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(255),
);
GO




-- Índices recomendados
CREATE INDEX IX_Usuario_GrupoID ON Usuario(GrupoID);
CREATE INDEX IX_Accion_ModuloID ON Accion(ModuloID);
CREATE INDEX IX_Permiso_GrupoID_AccionID ON Permiso(GrupoID, AccionID);

select * from Usuario