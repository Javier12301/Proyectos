-- NEGOCIO DEFAULT
INSERT INTO Negocio (Nombre, Direccion, Ciudad, CodigoPostal, NombreCompletoPropietario, Telefono, TipoDocumento, Documento)
VALUES ('Tienda de ropa', '-', '-', '-', '-', '3855219032', 'CUIT/CUIL', '22000402');
GO

-- METODOS
INSERT INTO MetodoPago (Nombre, Recargo) VALUES ('Efectivo', 0);
INSERT INTO MetodoPago (Nombre, Recargo) VALUES ('Tarjeta Débito', 0);
INSERT INTO MetodoPago (Nombre, Recargo) VALUES ('Tarjeta Crédito', 0);
INSERT INTO MetodoPago (Nombre, Recargo) VALUES ('Depósito/Transferencia', 0);
GO

select * from MetodoPago
