/*
El número de cubos debe duplicar el número 
máximo esperado de valores diferentes en la 
clave de índice, redondeando a la potencia de dos más próxima.
*/

CREATE TABLE [dbo].[Productos]
(
	[Id] INT NOT NULL PRIMARY KEY NONCLUSTERED HASH WITH (BUCKET_COUNT = 131072), 
    [Nombre] VARCHAR(max) NULL, 
    [Cantidad] BIGINT NULL, 
    [Vencimiento] DATETIME NULL, 
    [Estado] BIT NOT NULL
) WITH (MEMORY_OPTIMIZED = ON)

GO

-- Procedimiento de lectura, 

/*
No cambie las variables de nombre o ruta de acceso de la base de datos.
Cualquier variable sqlcmd será correctamente sustituida durante 
la compilación y la implementación.
*/

ALTER DATABASE [$(DatabaseName)]
	ADD FILEGROUP [Productos_FG] CONTAINS MEMORY_OPTIMIZED_DATA