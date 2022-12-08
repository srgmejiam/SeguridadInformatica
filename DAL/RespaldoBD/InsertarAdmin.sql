USE BDSeguridadInformatica
GO
INSERT INTO Usuario 
(Nombre, Login, Password, Email, Cargo, IdRol, Intentos, Bloqueado, Baja, Activo, UsuarioRegistro, FechaRegistro)
VALUES ('Administrador de Sistema', 'Admin1', 0x1622F6505B5F976FEC599A9A7E5E3FCB , 'correo@gmail.com', 'Administrador', 1, 0, 0, 0, 1, 1, GETDATE());