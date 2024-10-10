

INSERT INTO Usuario (Usuario, Contrasena, Nombre, Apellido, Correo, GrupoID, Estado)
VALUES 
    ('usuario11', 'contraseña1', 'Nombre1', 'Apellido1', 'correo1@example.com', 4, 1),
    ('usuario21', 'contraseña2', 'Nombre2', 'Apellido2', 'correo2@example.com', 4, 1),
    ('usuario31', 'contraseña3', 'Nombre3', 'Apellido3', 'correo3@example.com', 4, 1),
    ('usuario41', 'contraseña4', 'Nombre4', 'Apellido4', 'correo4@example.com', 4, 1),
    ('usuario51', 'contraseña5', 'Nombre5', 'Apellido5', 'correo5@example.com', 3, 1),
    ('usuario61', 'contraseña6', 'Nombre6', 'Apellido6', 'correo6@example.com', 3, 1),
    ('usuario71', 'contraseña7', 'Nombre7', 'Apellido7', 'correo7@example.com', 3, 1),
    ('usuario81', 'contraseña8', 'Nombre8', 'Apellido8', 'correo8@example.com', 3, 1),
    ('usuario91', 'contraseña9', 'Nombre9', 'Apellido9', 'correo9@example.com', 3, 1),
    ('usuario110', 'contraseña10', 'Nombre10', 'Apellido10', 'correo10@example.com', 1, 1);
GO GO GO;

Select * from Producto

SELECT COUNT(*) FROM Producto WHERE Nombre = 'producto 1'





DECLARE @Porcentaje DECIMAL(5, 2) = 25; -- Por ejemplo, un aumento del 20%
EXEC sp_ObtenerProductosConNuevoPrecio @Buscar = '', @FiltroCategoria = 'Camiseta de futbol', @Porcentaje = @Porcentaje;
