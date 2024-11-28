USE [TiendaDeRopa]
GO

INSERT INTO [dbo].[Usuario]
           ([Usuario]
           ,[Contrasena]
           ,[Nombre]
           ,[Apellido]
           ,[Correo]
           ,[GrupoID]
		   ,[Estado])
     VALUES ('Admin', 'Admin','Admin', '-','javierramirez1230123@gmail.com', 1, 1)
GO

INSERT INTO Grupo (Nombre, Estado)
VALUES ('Administrador', 1);
GO

-- Insertar m�dulos
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


-- Insertar las acciones para el m�dulo formVentas
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Vender', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formVentas'));
GO
-- Insertar las acciones para el m�dulo formProductos
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Alta', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formProductos')),
    ('Modificar', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formProductos')),
    ('Baja', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formProductos'));
GO
-- Insertar las acciones para el m�dulo formCategorias
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Alta', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formCategorias')),
    ('Modificar', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formCategorias')),
    ('Baja', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formCategorias'));
GO
-- Insertar las acciones para el m�dulo formInventario
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Comprar', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formInventario'));
GO
-- Insertar las acciones para el m�dulo formReportes
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Permitir_Acceso', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formReportes'));
GO
-- Insertar las acciones para el m�dulo formAjustes
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Permitir_Acceso', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formAjustes'));
GO
-- Insertar las acciones para el m�dulo formUsuarios
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Alta', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formUsuarios')),
    ('Modificar', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formUsuarios')),
    ('Baja', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formUsuarios'));
 GO
-- Insertar las acciones para el m�dulo formGrupos
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Alta', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formGrupos')),
    ('Modificar', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formGrupos')),
    ('Baja', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formGrupos'));
GO
-- Insertar las acciones para el m�dulo formAuditoria
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Permitir_Acceso', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formAuditoria'));
GO
-- Insertar las acciones para el m�dulo formMisDatos
INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Permitir_Acceso', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formMisDatos'));
GO

INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Permitir_Acceso', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formBackup'));
GO

INSERT INTO Accion(Nombre, ModuloID)
VALUES 
    ('Permitir_Acceso', (SELECT ModuloID FROM Modulo WHERE Nombre = 'formNegocio'));
GO

-- PERMISO TOTAL PARA ADMINISTRADORES
INSERT INTO Permiso (GrupoID, AccionID, Permitido)
SELECT 1, AccionID, 1
FROM Accion;
GO

select * from Permiso
Where GrupoID = 1

