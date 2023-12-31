USE [master]
GO
/****** Object:  Database [BD_TEST]    Script Date: 16/10/2023 1:47:05 p. m. ******/
CREATE DATABASE [BD_TEST]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BD_TEST', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\BD_TEST.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BD_TEST_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\BD_TEST_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BD_TEST] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BD_TEST].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BD_TEST] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BD_TEST] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BD_TEST] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BD_TEST] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BD_TEST] SET ARITHABORT OFF 
GO
ALTER DATABASE [BD_TEST] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BD_TEST] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BD_TEST] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BD_TEST] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BD_TEST] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BD_TEST] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BD_TEST] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BD_TEST] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BD_TEST] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BD_TEST] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BD_TEST] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BD_TEST] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BD_TEST] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BD_TEST] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BD_TEST] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BD_TEST] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BD_TEST] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BD_TEST] SET RECOVERY FULL 
GO
ALTER DATABASE [BD_TEST] SET  MULTI_USER 
GO
ALTER DATABASE [BD_TEST] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BD_TEST] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BD_TEST] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BD_TEST] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BD_TEST] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BD_TEST] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BD_TEST', N'ON'
GO
ALTER DATABASE [BD_TEST] SET QUERY_STORE = ON
GO
ALTER DATABASE [BD_TEST] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BD_TEST]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 16/10/2023 1:47:05 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[Contraseña] [varchar](max) NULL,
	[Estado] [bit] NOT NULL,
	[Identificacion] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuenta]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuenta](
	[IdCuenta] [int] IDENTITY(1,1) NOT NULL,
	[NumeroCuenta] [varchar](max) NULL,
	[TipoCuenta] [varchar](max) NULL,
	[SaldoInicial] [bigint] NULL,
	[Estado] [bit] NOT NULL,
	[Identificacion] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimientos]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimientos](
	[IdMovimiento] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
	[TipoMovimiento] [varchar](max) NULL,
	[Valor] [bigint] NULL,
	[Saldo] [bigint] NULL,
	[Identificacion] [varchar](50) NULL,
	[TipoCuenta] [varchar](max) NULL,
	[Estado] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMovimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Genero] [varchar](50) NULL,
	[Edad] [varchar](50) NULL,
	[Identificacion] [varchar](50) NULL,
	[Direccion] [varchar](max) NULL,
	[Telefono] [varchar](50) NULL,
	[Estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 

INSERT [dbo].[Cliente] ([IdCliente], [Contraseña], [Estado], [Identificacion]) VALUES (1, N'123456', 1, N'1014333000')
INSERT [dbo].[Cliente] ([IdCliente], [Contraseña], [Estado], [Identificacion]) VALUES (2, N'987654', 1, N'1014300468')
INSERT [dbo].[Cliente] ([IdCliente], [Contraseña], [Estado], [Identificacion]) VALUES (3, N'987654', 1, N'1014300468')
INSERT [dbo].[Cliente] ([IdCliente], [Contraseña], [Estado], [Identificacion]) VALUES (4, N'987654', 1, N'1014300468')
SET IDENTITY_INSERT [dbo].[Cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[Cuenta] ON 

INSERT [dbo].[Cuenta] ([IdCuenta], [NumeroCuenta], [TipoCuenta], [SaldoInicial], [Estado], [Identificacion]) VALUES (1, N'987654321', N'Corriente', 20200000, 1, N'1014333000')
INSERT [dbo].[Cuenta] ([IdCuenta], [NumeroCuenta], [TipoCuenta], [SaldoInicial], [Estado], [Identificacion]) VALUES (2, N'123456789', N'Ahorro', 1100000, 1, N'1014333000')
SET IDENTITY_INSERT [dbo].[Cuenta] OFF
GO
SET IDENTITY_INSERT [dbo].[Movimientos] ON 

INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (1, CAST(N'2023-12-10' AS Date), N'Transferencia', -1000000, 7001000, N'1014333000', NULL, NULL)
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (2, CAST(N'2023-12-10' AS Date), N'Transferencia', -1000000, 6001000, N'1014333000', NULL, NULL)
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (3, CAST(N'2023-12-10' AS Date), N'Transferencia', -1000000, 19000000, N'1014333000', NULL, NULL)
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (4, CAST(N'2023-12-10' AS Date), N'Transferencia', -1000000, 18000000, N'1014333000', NULL, NULL)
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (5, CAST(N'2023-12-10' AS Date), N'Transferencia', -1000000, 18000000, N'1014333000', NULL, NULL)
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (6, CAST(N'2023-12-10' AS Date), N'Transferencia', -1000000, 15000000, N'1014333000', NULL, NULL)
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (7, CAST(N'2023-12-10' AS Date), N'Transferencia', 5000000, 20000000, N'1014333000', N'Corriente', NULL)
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (8, CAST(N'2023-12-10' AS Date), N'Transferencia', 5000000, 0, N'1014333000', N'Ahorros', NULL)
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (9, CAST(N'2023-12-10' AS Date), N'Transferencia', 5000000, 23000000, N'1014333000', N'Ahorro', NULL)
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (10, CAST(N'2023-12-10' AS Date), N'Transferencia', -1900000, 21100000, N'1014333000', N'Ahorro', NULL)
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (11, CAST(N'2023-12-10' AS Date), N'Transferencia', -21200000, 0, N'101433300', N'Ahorro', N'Aprovado')
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (12, CAST(N'2023-12-10' AS Date), N'Transferencia', -21200000, 0, N'101433300', N'Ahorro', N'Aprovado')
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (13, CAST(N'2023-12-10' AS Date), N'Transferencia', -21200000, 0, N'101433300', N'Ahorro', N'Rechazado')
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (14, CAST(N'2023-12-10' AS Date), N'Transferencia', -21200000, 21100000, N'1014333000', N'Ahorro', N'Rechazado')
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (15, CAST(N'2023-12-10' AS Date), N'Transferencia', -21200000, 21100000, N'1014333000', N'Ahorro', N'Rechazado')
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (16, CAST(N'2023-12-10' AS Date), N'Transferencia', -20000000, 1100000, N'1014333000', N'Ahorro', N'Aprovado')
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (17, CAST(N'2023-12-10' AS Date), N'Transferencia', -200000, 900000, N'1014333000', N'Ahorro', N'Aprovado')
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (18, CAST(N'2023-10-15' AS Date), N'Transferencia', 200000, 1100000, N'1014333000', N'Ahorro', N'Aprovado')
INSERT [dbo].[Movimientos] ([IdMovimiento], [Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado]) VALUES (19, CAST(N'2023-10-15' AS Date), N'Transferencia', 200000, 20200000, N'1014333000', N'Corriente', N'Aprovado')
SET IDENTITY_INSERT [dbo].[Movimientos] OFF
GO
SET IDENTITY_INSERT [dbo].[Persona] ON 

INSERT [dbo].[Persona] ([IdUser], [Nombre], [Genero], [Edad], [Identificacion], [Direccion], [Telefono], [Estado]) VALUES (1, N'TEST', N'Masculino', N'30', N'1014333000', N'Direccion test', N'31266666', 1)
INSERT [dbo].[Persona] ([IdUser], [Nombre], [Genero], [Edad], [Identificacion], [Direccion], [Telefono], [Estado]) VALUES (5, N'Pepito Perez', N'Masculino', N'30', N'1014300468', N'Diagonal 123 abc', N'312617666', 1)
SET IDENTITY_INSERT [dbo].[Persona] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_create_account]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_create_account]
@numeroCuenta VARCHAR(MAX), 
@tipoCuenta VARCHAR(MAX),
@saldoInicial bigint,
@estado bit,
@identificacion varchar (50)
AS 
INSERT INTO Cuenta ([NumeroCuenta],[TipoCuenta],[SaldoInicial],[Estado], [Identificacion])
VALUES (@numeroCuenta, @tipoCuenta, @saldoInicial, @estado, @identificacion) 
GO
/****** Object:  StoredProcedure [dbo].[sp_create_cliente]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_create_cliente]
@contraseña VARCHAR(MAX), 
@estado bit,
@identificacion int
AS 
INSERT INTO Cliente ([Contraseña],[Estado],[Identificacion])
VALUES (@contraseña, @estado, @identificacion) 
GO
/****** Object:  StoredProcedure [dbo].[sp_create_movement]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_create_movement]
@fecha date, 
@TipoMovimiento VARCHAR(MAX),
@valor bigint,
@saldo bigint,
@Identificacion VARCHAR(MAX),
@tipoCuenta VARCHAR(MAX),
@estado varchar(10)
AS 
INSERT INTO Movimientos ([Fecha], [TipoMovimiento], [Valor], [Saldo], [Identificacion], [TipoCuenta], [Estado])
VALUES (@fecha, @TipoMovimiento, @valor, @saldo, @Identificacion, @tipoCuenta, @estado)
GO
/****** Object:  StoredProcedure [dbo].[sp_create_persona]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_create_persona]
@nombre VARCHAR(MAX), 
@genero VARCHAR(50),
@edad VARCHAR(50),
@identificacion VARCHAR(50),
@direccion VARCHAR(MAX),
@telefono VARCHAR(50),
@estado bit
AS 
INSERT INTO Persona ([Nombre],[Genero],[Edad],[Identificacion],[Direccion],[Telefono],[Estado])
VALUES (@nombre, @genero, @Edad, @Identificacion, @Direccion, @Telefono, @Estado)
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_account]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_delete_account]
@numeroCuenta varchar(50)
AS 
DELETE FROM Cuenta WHERE NumeroCuenta = @numeroCuenta
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_movement]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_delete_movement]
@fecha date
AS 
DELETE FROM Movimientos WHERE Fecha = @fecha
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_persona]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_delete_persona]
@Identificacion varchar(50)
AS 
DELETE FROM Persona WHERE Identificacion = @Identificacion
GO
/****** Object:  StoredProcedure [dbo].[sp_select_account]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_select_account]
@identificacion varchar(MAX)
AS 
SELECT * FROM Cuenta WHERE Identificacion = @identificacion
GO
/****** Object:  StoredProcedure [dbo].[sp_select_movement]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_select_movement]
@fechaInicial date,
@fechaFinal date
AS 
SELECT * 
FROM Movimientos 
--INNER JOIN Cuenta
--ON Movimientos.Identificacion = Cuenta.Identificacion
INNER JOIN Persona
ON Movimientos.Identificacion = Persona.Identificacion
WHERE Fecha BETWEEN @fechaInicial AND @fechaFinal
GO
/****** Object:  StoredProcedure [dbo].[sp_select_persona]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_select_persona]
@contraseña varchar(MAX)
AS
BEGIN
SELECT *
FROM Persona
INNER JOIN Cliente
ON Persona.Identificacion = Cliente.Identificacion
WHERE Contraseña = @contraseña
END
GO
/****** Object:  StoredProcedure [dbo].[sp_update_account]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_update_account]
@Identificacion VARCHAR(MAX), 
@saldoInicial bigint,
@tipoCuenta varchar(MAX)
AS 
UPDATE Cuenta SET 
[SaldoInicial] = @saldoInicial
WHERE Identificacion = @Identificacion AND TipoCuenta = @tipoCuenta
GO
/****** Object:  StoredProcedure [dbo].[sp_update_movement]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_update_movement]
@fecha date, 
@tipoMovimiento VARCHAR(MAX),
@valor bigint,
@identificacion VARCHAR(50),
@tipoCuenta VARCHAR(50),
@estado VARCHAR(10)
AS 
UPDATE Movimientos SET 
[Fecha] = @fecha,
[TipoMovimiento] = @tipoMovimiento,
[Valor] = @valor,
[Identificacion] = @identificacion,
[TipoCuenta] = @tipoCuenta,
[Estado] = @estado
WHERE Fecha = @fecha
GO
/****** Object:  StoredProcedure [dbo].[sp_update_persona]    Script Date: 16/10/2023 1:47:06 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_update_persona]
@nombre VARCHAR(MAX), 
@genero VARCHAR(50),
@edad VARCHAR(50),
@identificacion VARCHAR(50),
@direccion VARCHAR(MAX),
@telefono VARCHAR(50),
@estado bit
AS 
UPDATE Persona SET 
[Nombre] = @nombre,
[Genero] = @genero,
[Edad] = @Edad,
[Identificacion] = @Identificacion,
[Direccion] = @Direccion,
[Telefono] = @Telefono,
[Estado] = @Estado
WHERE Identificacion = @Identificacion
GO
USE [master]
GO
ALTER DATABASE [BD_TEST] SET  READ_WRITE 
GO
