CREATE DATABASE JSanchezCine
USE JSanchezCine
GO
CREATE TABLE Usuario(
	IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
	UserName VARCHAR(50) NOT NULL,
	Password VARCHAR(50) NOT NULL
)
GO

CREATE TABLE Zona(
	IdZona INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(50)
)
GO
INSERT INTO Zona(Nombre)VALUES('Sur')
SELECT * FROM Zona
GO

CREATE TABLE Cine(
	IdCine INT IDENTITY(1,1) PRIMARY KEY,
	Latitud VARCHAR(100),
	Longitud VARCHAR(100),
	Direccion VARCHAR(100),
	Venta VARCHAR(100),
	IdZona INT,
	CONSTRAINT fk_CineZona FOREIGN KEY (IdZona) REFERENCES Zona (IdZona)
)
GO

CREATE PROCEDURE UsuarioAdd
@UserName VARCHAR(50),
@Password VARCHAR(50)
AS
INSERT INTO Usuario(UserName,Password) VALUES(@UserName,@Password)
GO

CREATE PROCEDURE UsuarioGetByUserName --'JSanchez'
@UserName VARCHAR(50)
AS
SELECT 
IdUsuario,
UserName,
Password
FROM Usuario
WHERE UserName=@UserName
GO

ALTER PROCEDURE CineGetAll
AS
SELECT IdCine,Latitud,Longitud,Direccion,Venta,Zona.IdZona,Zona.Nombre,Porcentaje=0.0,Ventas=0.0,Zonas='' FROM Cine
INNER JOIN Zona ON Cine.IdZona = Zona.IdZona
GO

ALTER PROCEDURE CineGetById --1
@IdCine INT
AS
SELECT IdCine,Latitud,Longitud,Direccion,Venta,Zona.IdZona,Zona.Nombre,Porcentaje=0.0,Ventas=0.0,Zonas='' FROM Cine
INNER JOIN Zona ON Cine.IdZona = Zona.IdZona
WHERE IdCine = @IdCine
GO

CREATE PROCEDURE CineAdd '123.12312','5353.123123','Siempre viva 12','20',1
@Latitud VARCHAR(100),
@Longitud VARCHAR(100),
@Direccion VARCHAR(100),
@Venta VARCHAR(100),
@IdZona INT
AS
INSERT INTO Cine(
				 Latitud,
				 Longitud,
				 Direccion,
				 Venta,
				 IdZona) VALUES(
							  @Latitud,
							  @Longitud,
							  @Direccion,
							  @Venta,
							  @IdZona)
GO

CREATE PROCEDURE CineUpdate
@Latitud VARCHAR(100),
@Longitud VARCHAR(100),
@Direccion VARCHAR(100),
@Venta VARCHAR(100),
@IdZona INT
AS
UPDATE Cine
SET
Latitud=@Latitud,
Longitud=@Longitud,
Direccion=@Direccion,
Venta=@Venta,
IdZona=@IdZona
GO

CREATE PROCEDURE CineDelete
@IdCine INT
AS
DELETE FROM Cine WHERE IdCine=@IdCine
GO

CREATE PROCEDURE ZonaGetAll
AS
SELECT IdZona,Nombre FROM Zona
GO

ALTER PROCEDURE VentasGet
AS
SELECT SUM(CONVERT(DECIMAL,Cine.Venta)) AS Ventas, Zona.Nombre AS Zonas, COUNT(*) * 100.0 / (SELECT COUNT(*) FROM Cine) AS Porcentaje, 
CAST(ROW_NUMBER() OVER (ORDER BY Cine.Venta) AS INT) IdCine,Direccion='',IdZona=0,Latitud='',Longitud='',Nombre='',Venta=''
FROM Cine
JOIN Zona ON Cine.IdZona = Zona.IdZona
GROUP BY Cine.Venta, Zona.Nombre
GO

SELECT (SELECT COUNT(*) FROM Cine) --numero de datos
SELECT SUM(CONVERT(DECIMAL,Cine.Venta)) FROM Cine --Total de ventas
SELECT Venta FROM Cine

SELECT (SELECT SUM(CONVERT(DECIMAL,Venta)) FROM Cine) / (SELECT COUNT(*) FROM Cine) * 100.0


ALTER PROCEDURE VentasGet
AS
SELECT SUM(CONVERT(DECIMAL,Cine.Venta)) AS Ventas, Zona.Nombre AS Zonas, (SELECT SUM(CONVERT(DECIMAL,Venta)) FROM Cine) / (SELECT COUNT(*) FROM Cine) * 100.0 AS Porcentaje, 
CAST(ROW_NUMBER() OVER (ORDER BY Cine.Venta) AS INT) IdCine,Direccion='',IdZona=0,Latitud='',Longitud='',Nombre='',Venta=''
FROM Cine
JOIN Zona ON Cine.IdZona = Zona.IdZona
GROUP BY Cine.Venta, Zona.Nombre
GO

SELECT Zona.Nombre, (COUNT(Cine.Venta)*100.0)/(SELECT COUNT(*) FROM Cine WHERE Cine.IdZona=Zona.IdZona) AS PorcentajeVenta
FROM Cine
INNER JOIN Zona ON Cine.IdZona=Zona.IdZona
GROUP BY Zona.Nombre
ORDER BY PorcentajeVenta DESC;