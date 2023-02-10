
CREATE DATABASE dbFinanciero

GO 

--Creacion de tablas
USE dbFinanciero
CREATE TABLE Cliente
(
	IdCliente INT PRIMARY KEY IDENTITY(1,1), 
	strTipoIdentificacion VARCHAR(10), 
	strNumeroIdentificacion VARCHAR(100), 
	strNombre VARCHAR(100), 
	strApellido VARCHAR(100), 
	strEmail VARCHAR(200), 
	dtFechaNacimiento SMALLDATETIME,
	dtFechaCreacion SMALLDATETIME, 
	dtFechaModificacion SMALLDATETIME
)

CREATE TABLE TipoProducto
(
	IdTipoProducto INT PRIMARY KEY IDENTITY(1,1),
	strNombre VARCHAR(200)
)

CREATE TABLE ProductoEstado
(
	IdProductoEstado INT PRIMARY KEY IDENTITY(1,1),
	strNombre VARCHAR(50)
)

CREATE TABLE Producto
(
	IdProducto INT PRIMARY KEY IDENTITY(1,1), 
	IdTipoProducto INT FOREIGN KEY REFERENCES TipoProducto(IdTipoProducto), 
	NumeroCuenta VARCHAR(10) UNIQUE, 
	intEstado INT, 
	numSaldo NUMERIC(18,2), 
	ExentaGMF VARCHAR(200), 
	dtFechaCreacion SMALLDATETIME, 
	dtFechaModificacion SMALLDATETIME, 
	IdCliente INT FOREIGN KEY REFERENCES Cliente(IdCliente)
)

CREATE TABLE TipoTransaccion
(
	IdTipoTransaccion INT PRIMARY KEY IDENTITY(1,1),
	strNombre VARCHAR(200)
)

CREATE TABLE Transaccion
(
	IdTransaccion INT PRIMARY KEY IDENTITY(1,1),
	IdTipoTransaccion INT FOREIGN KEY REFERENCES TipoTransaccion(IdTipoTransaccion),
	IdProductoOrigen INT,
	IdProductoDestino INT,
	numSaldo NUMERIC(18,2)
)

GO

--CREACION DE PROCEDIMIENTOS

CREATE PROCEDURE ClienteAdd
(
	@strTipoIdentificacion VARCHAR(10), 
	@strNumeroIdentificacion VARCHAR(100), 
	@strNombre VARCHAR(100), 
	@strApellido VARCHAR(100), 
	@strEmail VARCHAR(200), 
	@dtFechaNacimiento SMALLDATETIME
)
AS

IF(((select (((365* year(getdate())) - (365*(year(@dtFechaNacimiento)))) + (month(getdate())-month(@dtFechaNacimiento))*30 + (day(getdate()) -  day(@dtFechaNacimiento)))/365) > 18)
	AND (SELECT COUNT(*) FROM Cliente WHERE strNumeroIdentificacion = @strNumeroIdentificacion) = 0)
BEGIN
	INSERT INTO Cliente
	(
		strTipoIdentificacion, 
		strNumeroIdentificacion, 
		strNombre, 
		strApellido, 
		strEmail, 
		dtFechaNacimiento, 
		dtFechaCreacion, 
		dtFechaModificacion
	)
	VALUES
	(
		@strTipoIdentificacion, 
		@strNumeroIdentificacion, 
		@strNombre, 
		@strApellido, 
		@strEmail, 
		@dtFechaNacimiento,
		GETDATE(), 
		GETDATE()
	)

	SELECT @@IDENTITY

END
ELSE
	BEGIN 		
		DECLARE @err_message VARCHAR(255)
		SET @err_message = 'No se puede insertar un cliente menor de edad, o el cliente ya existe'
		RAISERROR (@err_message, 11,1)
	END

GO

CREATE PROCEDURE ClienteUpdate
(
	@IdCliente INT,
	@strTipoIdentificacion VARCHAR(10), 
	@strNumeroIdentificacion VARCHAR(100), 
	@strNombre VARCHAR(100), 
	@strApellido VARCHAR(100), 
	@strEmail VARCHAR(200), 
	@dtFechaNacimiento SMALLDATETIME
)
AS 
IF((SELECT COUNT(*) FROM Cliente WHERE IdCliente = @IdCliente) > 0)
BEGIN
UPDATE Cliente
SET		strTipoIdentificacion = @strTipoIdentificacion,  
		strNumeroIdentificacion = @strNumeroIdentificacion, 
		strNombre = @strNombre, 
		strApellido = @strApellido, 
		strEmail = @strEmail, 
		dtFechaNacimiento = @dtFechaNacimiento, 		
		dtFechaModificacion = GETDATE()
WHERE IdCliente = @IdCliente
END
ELSE
	BEGIN 		
		DECLARE @err_message VARCHAR(255)
		SET @err_message = 'El cliente no existe'
		RAISERROR (@err_message, 11,1)
	END
GO

CREATE PROCEDURE ClienteDelete
(
	@IdCliente INT
)

AS

IF ((SELECT COUNT(*) FROM Producto WHERE IdCliente = @IdCliente) = 0)
BEGIN
	DELETE FROM Cliente 
	WHERE IdCliente = @IdCliente
END
ELSE
	BEGIN 		
		DECLARE @err_message VARCHAR(255)
		SET @err_message = 'El cliente tiene productos asociados'
		RAISERROR (@err_message, 11,1)
	END


GO

CREATE PROCEDURE ClienteGet
AS

SELECT  IdCliente,
		strTipoIdentificacion, 
		strNumeroIdentificacion, 
		strNombre, 
		strApellido, 
		strEmail, 
		dtFechaNacimiento, 
		dtFechaCreacion, 
		dtFechaModificacion 
FROM Cliente

GO

CREATE PROCEDURE ClienteGetSingle
(
	@IdCliente INT
)
AS

SELECT  IdCliente,
		strTipoIdentificacion, 
		strNumeroIdentificacion, 
		strNombre, 
		strApellido, 
		strEmail, 
		dtFechaNacimiento, 
		dtFechaCreacion, 
		dtFechaModificacion 
FROM Cliente
WHERE IdCliente = @IdCliente

GO

--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

CREATE PROCEDURE TipoProductoGet
AS 

SELECT   IdTipoProducto,
		 strNombre
FROM TipoProducto

GO

--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

CREATE PROCEDURE ProductoEstadoGet
AS
SELECT   IdProductoEstado,
		 strNombre
FROM ProductoEstado

GO

--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

CREATE PROCEDURE ProductoAdd
(
	@IdTipoProducto INT, 	
	@numSaldo NUMERIC(18,2), 
	@ExentaGMF VARCHAR(200), 
	@IdCliente INT
)

AS

IF((SELECT COUNT(*) FROM TipoProducto WHERE IdTipoProducto = @IdTipoProducto) > 0 AND (SELECT COUNT(*) FROM Cliente WHERE IdCliente = @IdCliente) > 0)
BEGIN
	
	DECLARE @intConsecutivo INT, @NumeroCuenta VARCHAR(20)

	-- 1 = Tipo de cuenta ahorro
	-- 2 = Tipo de cuenta corriente

	IF(@IdTipoProducto = 1)
	BEGIN
		SELECT  @intConsecutivo=ISNULL(CAST(CAST(SUBSTRING(MAX(NumeroCuenta),3,LEN(MAX(NumeroCuenta))) AS INT) + 1 AS VARCHAR),'00000001') FROM Producto WHERE SUBSTRING(NumeroCuenta, 1, 2) = '53'
		SET @NumeroCuenta = '53' + RIGHT('00000000', 8 - len(cast(@intConsecutivo AS VARCHAR)))+CAST(@intConsecutivo AS VARCHAR)
	END
	ELSE
	BEGIN
		SELECT  @intConsecutivo=ISNULL(CAST(CAST(SUBSTRING(MAX(NumeroCuenta),3,LEN(MAX(NumeroCuenta))) AS INT) + 1 AS VARCHAR),'00000001') FROM Producto WHERE SUBSTRING(NumeroCuenta, 1, 2) = '33'
		SET @NumeroCuenta = '33' + RIGHT('00000000', 8 - len(cast(@intConsecutivo AS VARCHAR)))+CAST(@intConsecutivo AS VARCHAR)
	END

	INSERT INTO Producto
	(
		IdTipoProducto, 
		NumeroCuenta, 
		intEstado, 
		numSaldo, 
		ExentaGMF, 
		dtFechaCreacion, 
		dtFechaModificacion, 
		IdCliente
	)
	VALUES
	(
		@IdTipoProducto, 
		@NumeroCuenta, 
		1, 
		@numSaldo, 
		@ExentaGMF, 
		GETDATE(), 
		GETDATE(),
		@IdCliente
	)

	SELECT @@IDENTITY

END
ELSE
	BEGIN 		
		DECLARE @err_message VARCHAR(255)
		SET @err_message = 'El cliente no existe o el tipo de cuenta es incorrecto'
		RAISERROR (@err_message, 11,1)
	END

GO

CREATE PROCEDURE ProductoUpdateEstado
(
	@IdProducto INT,
	@intEstado INT
)

AS

DECLARE @intError INT = 0

IF(@intEstado = 3)
BEGIN
	SET @intError = ((SELECT 1 FROM Producto WHERE IdProducto = @IdProducto AND numSaldo = 0))
END

IF(@intError = 0)
BEGIN
	UPDATE Producto
	SET intEstado = @intEstado
	WHERE IdProducto = @IdProducto
END
ELSE
	BEGIN 		
		DECLARE @err_message VARCHAR(255)
		SET @err_message = 'El saldo de la cuenta debe ser igual a cero'
		RAISERROR (@err_message, 11,1)
	END

GO

CREATE PROCEDURE ProductoUpdateSaldo
(
	@IdProducto INT,
	@numSaldo NUMERIC(18,2),
	@intTipo INT
)
AS
-- @intTipo = 1 Suma el saldo, @intTipo = 2 Resta el saldo

DECLARE @numSaldoNew NUMERIC(18,2)

SET @numSaldoNew = (SELECT numSaldo FROM Producto WHERE IdProducto = @IdProducto)

IF(@intTipo = 1)
BEGIN	
	SET @numSaldoNew = @numSaldoNew + @numSaldo
END
ELSE IF(@intTipo = 2)
BEGIN	
	SET @numSaldoNew = @numSaldoNew - @numSaldo
END

UPDATE Producto
SET numSaldo = @numSaldoNew
WHERE IdProducto = @IdProducto

GO

CREATE PROCEDURE ProductoGetByCliente
(
	@IdCliente INT
)
AS 
SELECT  IdProducto,
		IdTipoProducto, 
		NumeroCuenta, 
		intEstado, 
		numSaldo, 
		ExentaGMF, 
		dtFechaCreacion, 
		dtFechaModificacion,
		IdCliente
FROM Producto
WHERE IdCliente = @IdCliente

GO

CREATE PROCEDURE ProductoGet
AS 
SELECT  IdProducto,
		IdTipoProducto, 
		NumeroCuenta, 
		intEstado, 
		numSaldo, 
		ExentaGMF, 
		dtFechaCreacion, 
		dtFechaModificacion,
		IdCliente
FROM Producto

GO

--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

CREATE PROCEDURE TipoTransaccionGet
AS
SELECT   IdTipoTransaccion,
		 strNombre
FROM TipoTransaccion

GO
--//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

CREATE PROCEDURE TransaccionAdd
(
	@IdTipoTransaccion INT,
	@IdProductoOrigen INT,
	@IdProductoDestino INT,
	@numSaldo NUMERIC(18,2)
)
AS

DECLARE @strError VARCHAR(200) = ''

IF((SELECT COUNT(*) FROM TipoTransaccion WHERE IdTipoTransaccion = @IdTipoTransaccion) = 0)
BEGIN
	SET @strError = 'El tipo de transacción no existe <br/>'
END

IF((SELECT COUNT(*) FROM Producto WHERE IdProducto = @IdProductoOrigen AND intEstado = 1) = 0 AND (@IdTipoTransaccion <> 1))
BEGIN
	SET @strError = 'El producto origen no existe <br/>'
END

IF((SELECT COUNT(*) FROM Producto WHERE IdProducto = @IdProductoDestino AND intEstado = 1) = 0 AND (@IdTipoTransaccion <> 2))
BEGIN
	SET @strError = 'El producto destino no existe <br/>'
END

IF(@IdTipoTransaccion <> 1)
BEGIN
	DECLARE @numSaldoNew NUMERIC(18,2) = (SELECT numSaldo FROM Producto WHERE IdProducto = @IdProductoOrigen)

	IF(@numSaldoNew - @numSaldo < 0)
	BEGIN	
		SET @strError = 'El producto origen no tiene el saldo suficiente <br/>'
	END
END

IF(@strError = '')
BEGIN
	
	IF(@IdTipoTransaccion = 1)BEGIN SET @IdProductoOrigen = 0 END
	IF(@IdTipoTransaccion = 2)BEGIN SET @IdProductoDestino = 0 END

	IF(@IdTipoTransaccion <> 1)
	BEGIN		
		--Inserta e actualiza en producto origen
		EXECUTE ProductoUpdateSaldo @IdProductoOrigen, @numSaldo, 2
	END

	IF(@IdTipoTransaccion <> 2)
	BEGIN		
		EXECUTE ProductoUpdateSaldo @IdProductoDestino, @numSaldo, 1
	END

	--Inserta e actualiza en producto destino
	INSERT INTO Transaccion
	(
		IdTipoTransaccion,
		IdProductoOrigen,
		IdProductoDestino,
		numSaldo
	)
	VALUES
	(
		@IdTipoTransaccion,
		@IdProductoOrigen,
		@IdProductoDestino,
		@numSaldo
	)

	SELECT @@IDENTITY

END
ELSE
	BEGIN 		
		DECLARE @err_message VARCHAR(255)
		SET @err_message = @strError
		RAISERROR (@err_message, 11,1)
	END
GO

CREATE PROCEDURE TransaccionGetByProducto
(
	@IdProducto INT
)
AS

SELECT  IdTransaccion,
		IdTipoTransaccion,  
		numSaldo,
		IdProductoOrigen,
		IdProductoDestino
FROM Transaccion TRANS
WHERE IdProductoOrigen = @IdProducto OR IdProductoDestino = @IdProducto

--------------//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
GO

----Llenada de datos en tablas maestras
INSERT INTO TipoTransaccion (strNombre) VALUES ('Consignación')
INSERT INTO TipoTransaccion (strNombre) VALUES ('Retiro')
INSERT INTO TipoTransaccion (strNombre) VALUES ('Transferencia entre cuentas')

INSERT INTO ProductoEstado(strNombre) VALUES ('Activa')
INSERT INTO ProductoEstado(strNombre) VALUES ('Inactiva')
INSERT INTO ProductoEstado(strNombre) VALUES ('Cancelada')

INSERT INTO TipoProducto(strNombre) VALUES ('Cuenta de ahorros')
INSERT INTO TipoProducto(strNombre) VALUES ('Cuenta corriente')


