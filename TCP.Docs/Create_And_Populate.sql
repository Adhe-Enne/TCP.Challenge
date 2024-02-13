USE [Master]
GO

IF DB_ID('TCP.FacturadorDB') IS NOT NULL
	BEGIN 
	DROP DATABASE [TCP.FacturadorDB]
	PRINT 'La base de datos fue reiniciada'
	END
GO
CREATE DATABASE [TCP.FacturadorDB] 
GO
USE [TCP.FacturadorDB]
GO

CREATE TABLE [dbo].[Customer](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[Name] [VARCHAR](50),
	[Address] [VARCHAR](50),
	[City] [VARCHAR](50),
	[Email] [VARCHAR](50),
	[Phone] [VARCHAR](50),
	[Status] [INT] NOT NULL,
	[DateAdded] [DATETIME],
	[DateUpdated] [DATETIME]
	
CONSTRAINT PK_Customer PRIMARY KEY ([Id]),
)
GO

CREATE TABLE [dbo].[Product](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[Price] [DECIMAL],
	[Code] [VARCHAR](50),
	[Description] [VARCHAR](50),
	[Status] [INT] NOT NULL,
	[DateAdded] [DATETIME],
	[DateUpdated] [DATETIME]
		
CONSTRAINT PK_Product PRIMARY KEY ([Id])
)	
GO

CREATE TABLE [dbo].[Client](
	[Id] [INT] IDENTITY(1000,1) NOT NULL,
	[CompanyName] [VARCHAR](50),
	[CUIT] [VARCHAR](20),
	[Adress] [VARCHAR](50),
	[Phone] [VARCHAR](20),
	[Email] [VARCHAR](50),
	[Status] [INT] NOT NULL,
	[DateAdded] [DATETIME],
	[DateUpdated] [DATETIME]

CONSTRAINT PK_Client PRIMARY KEY ([Id])
)
GO


CREATE TABLE [dbo].[Invoice](
	[Id] [INT] IDENTITY(2000,1) NOT NULL,
	[ClientId] [INT] NOT NULL,
	[CustomerId] [INT] NOT NULL,
	[TotalQty] [INT],
	[TotalAmount] [DECIMAL],
	[PaymentMethod] [INT] NOT NULL,
	[Status] [INT] NOT NULL,
	[InvoiceStatus] [INT] NOT NULL,
	[DateAdded] [DATETIME],
	[DateUpdated] [DATETIME],
	[DueDate] [DATETIME],
	
CONSTRAINT PK_Invoice PRIMARY KEY ([Id]),
CONSTRAINT FK_Invoice_Client FOREIGN KEY (ClientId) REFERENCES Client (Id),
CONSTRAINT FK_Invoice_Customer FOREIGN KEY (CustomerId) REFERENCES Customer (Id)
)

GO


CREATE TABLE [dbo].[Invoice_Detail](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [INT] NOT NULL,
	[ProductId] [INT] NOT NULL,
	[Qty] [DECIMAL],
	[UnitPrice] [DECIMAL],
	[LineAmount] [DECIMAL](30),
	[Status] [INT] NOT NULL,
	[DateAdded] [DATETIME],
	[DateUpdated] [DATETIME]
		
CONSTRAINT PK_Invoice_Detail PRIMARY KEY ([Id]),
CONSTRAINT FK_Invoice FOREIGN KEY(InvoiceId) REFERENCES Invoice (Id),
CONSTRAINT FK_Product FOREIGN KEY(ProductId) REFERENCES Product (Id)
)	
GO

CREATE TABLE [dbo].[ListOption](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[OptionType] [VARCHAR](50),
	[OptionId] [INT],
	[Code] [VARCHAR](50),
	[Name] [VARCHAR](50),
	[Description] [VARCHAR](255),
	[Status] [INT] NOT NULL,
	[DateAdded] [DATETIME],
	[DateUpdated] [DATETIME]
CONSTRAINT PK_ListOption PRIMARY KEY ([Id])
)
GO

CREATE VIEW [InvoiceLineMountTotalsView] AS
SELECT I.Id [Factura], I.DateAdded [FechaAlta],LP.[Name] [Estado], LP.[Description] [Descripcion], SUM(D.LineAmount) MontoTotal
FROM Invoice I
INNER JOIN Invoice_Detail D on I.Id = D.InvoiceId
INNER JOIN ListOption LP ON LP.OptionId = I.InvoiceStatus
GROUP BY I.Id, I.DateAdded,LP.[Name], LP.[Description]
GO


CREATE VIEW [InvoiceClientView] AS
SELECT I.Id [Factura], I.DateAdded [FechaAlta], C.CompanyName [RazonSocial], C.CUIT
FROM Invoice I
INNER JOIN Client C on C.Id = I.ClientId
GO

Select * From InvoiceLineMountTotalsView;
Select * From InvoiceClientView;


CREATE PROCEDURE FacturaPorClienteProductoMasVendido @FechaDesde nvarchar(30), @FechaHasta nvarchar(30), @IdCliente int
AS
/*Teniendo en cuenta los campos que agregue para ampliar la logica, selecciono unicamente los mas relevantes*/
	SELECT
    I.Id AS Factura,
    I.ClientId Cliente,
    I.CustomerId Vendedor,
    I.TotalAmount,
    D.Qty,D.ProductId, D.Qty ,P.Code TopSellingProduct, P.Description
FROM Invoice I
INNER JOIN Invoice_Detail D ON I.Id = D.InvoiceId
INNER JOIN Product P ON P.Id = D.ProductId
WHERE
    (I.DateAdded BETWEEN @FechaDesde AND @FechaHasta)
    AND I.Id = @IdCliente
    AND D.Qty = (
        SELECT TOP 1 ID.Qty
        FROM Invoice_Detail ID
        WHERE ID.InvoiceId = I.Id
        ORDER BY ID.Qty DESC
    );
GO

CREATE PROCEDURE FacturaPorClienteProductoMasVendidoList @FechaDesde nvarchar(30), @FechaHasta nvarchar(30)
AS
/*Teniendo en cuenta los campos que agregue para ampliar la logica, selecciono unicamente los mas relevantes*/
	SELECT
    I.Id AS Factura,
    I.ClientId Cliente,
    I.CustomerId Vendedor,
    I.TotalAmount,
    D.Qty,D.ProductId ,P.Code TopSellingProduct, P.Description
FROM Invoice I
INNER JOIN Invoice_Detail D ON I.Id = D.InvoiceId
INNER JOIN Product P ON P.Id = D.ProductId
WHERE
    (I.DateAdded BETWEEN @FechaDesde AND @FechaHasta)
    AND D.Qty = (
        SELECT TOP 1 ID.Qty
        FROM Invoice_Detail ID
        WHERE ID.InvoiceId = I.Id
        ORDER BY ID.Qty DESC
    );
GO

select * from Client;
select * from Invoice;
select * from Product;
select * from Invoice_Detail;
select * from Customer;
select * from ListOption;
-- Inserts para la tabla Customer
INSERT INTO [dbo].[Customer] ([Name], [Address], [City], [Email], [Phone], [Status], [DateAdded], [DateUpdated])
VALUES
('CompraGamer', 'Av Francisco Beiro 5763', 'Buenos Aires', 'Fravega@business.net', '123456789', 1, GETDATE(), GETDATE()),
('Maximus', 'Av Córdoba 1527','Entre Rios', 'Coto@business.net', '987654321', 1, GETDATE(), GETDATE()),
('Gezatek', 'Santiago del Estero 2771','Chubut', 'Compumundo@business.net', '987654321', 1, GETDATE(), GETDATE()),
('Compugarden', 'Sarmiento 329','Buenos Aires', 'Jumbo@business.net', '01153004000', 1, GETDATE(), GETDATE());

-- Inserts para la tabla Product
INSERT INTO [dbo].[Product] ([Price], [Code], [Description], [Status], [DateAdded], [DateUpdated])
VALUES
(999.99, 'T4K', 'Televisor 4k', 1, GETDATE(), GETDATE()),
(899.99, 'PS4', 'PlayStation 4 Standard', 1, GETDATE(), GETDATE()),
(899.99, 'XBOX1', 'XBox One', 1, GETDATE(), GETDATE()),
(899.99, 'XBOXSX', 'XBox Series S', 1, GETDATE(), GETDATE()),
(899.99, 'XBOXSS', 'XBox Series X', 1, GETDATE(), GETDATE()),
(799.99, 'GRTX3070', 'GigaByte Rtx 3070 8GB', 1, GETDATE(), GETDATE()),
(499.99, 'ARTX3050', 'Aorus Rtx 3050 2GB', 1, GETDATE(), GETDATE()),
(1499.99, 'FRTX3090', 'Nvidia Founders Edition Rtx 3090 12GB', 1, GETDATE(), GETDATE()),
(399.99, 'ASUSMB-B450', 'ASUS ROG MotherBoard B450', 1, GETDATE(), GETDATE()),
(499.99, 'ASUSMON24', 'ASUS Monitor 24" 75hz', 1, GETDATE(), GETDATE()),
(9.99, 'USBCABLE', 'Cable Usb 2.0 Standar', 1, GETDATE(), GETDATE()),
(14.99, 'USBCABLEC', 'Cable Usb 3.0 C', 1, GETDATE(), GETDATE()),
(29.99, 'USBCABLETB', 'Cable Usb 3.0 C ThunderBolt', 1, GETDATE(), GETDATE()),
(29.99, 'PENDRV', 'Pendrive 24GB 3.0', 1, GETDATE(), GETDATE()),
(29.99, 'CKEYBRD', 'Corsair Teclado Rgb', 1, GETDATE(), GETDATE()),
(29.99, 'CMS', 'Corsair Mouse Rgb', 1, GETDATE(), GETDATE()),
(149.99, 'CHEADPHN', 'Corsair Auriculares Overhead', 1, GETDATE(), GETDATE()),
(29.99, 'MSPDXL', 'Mouse Pad Extra Large XL', 1, GETDATE(), GETDATE()),
(29.99, 'FANRGB', 'Chasis Cooler Rgb', 1, GETDATE(), GETDATE()),
(29.99, 'XBOX1JS', 'Xbox One Joystick', 1, GETDATE(), GETDATE()),
(29.99, 'R7CPU', 'AMD Ryzen 7 5700', 1, GETDATE(), GETDATE()),
(29.99, 'R5CPU', 'AMD Ryzen 5 5600x', 1, GETDATE(), GETDATE()),
(29.99, 'R3CPU', 'AMD Ryzen 5 5200 APU', 1, GETDATE(), GETDATE()),
(49.99, 'KDDR4', 'Kingston Memoria Ram DDR4 16Gb', 1, GETDATE(), GETDATE()),
(1499.99, 'LGTV65', 'LG 65" 4K UHD Smart TV', 1, GETDATE(), GETDATE()),
(399.99, 'NINSWITCH', 'Nintendo Switch Console', 1, GETDATE(), GETDATE()),
(599.99, 'PS5', 'PlayStation 5 Console', 1, GETDATE(), GETDATE()),
(299.99, 'LOGIMOUSE', 'Logitech Wireless Mouse', 1, GETDATE(), GETDATE()),
(149.99, 'RZRGAMINGKB', 'Razer Gaming Keyboard', 1, GETDATE(), GETDATE()),
(79.99, 'SEAGATE1TB', 'Seagate 1TB External HDD', 1, GETDATE(), GETDATE()),
(129.99, 'CORSHEADOSET', 'Corsair Gaming Headset', 1, GETDATE(), GETDATE()),
(699.99, 'EVGARTX3080', 'EVGA GeForce RTX 3080', 1, GETDATE(), GETDATE()),
(129.99, 'RZRMAMBA', 'Razer Mamba Elite Gaming Mouse', 1, GETDATE(), GETDATE()),
(799.99, 'AMDR93900X', 'AMD Ryzen 9 3900X CPU', 1, GETDATE(), GETDATE()),
(59.99, 'HPPRINTER', 'HP OfficeJet Pro Printer', 1, GETDATE(), GETDATE()),
(199.99, 'SAMSUNGSSD1TB', 'Samsung 1TB SSD', 1, GETDATE(), GETDATE()),
(399.99, 'ASUSROG144HZ', 'ASUS ROG 27" 144Hz Gaming Monitor', 1, GETDATE(), GETDATE()),
(79.99, 'HYPERXRAM16GB', 'HyperX 16GB DDR4 RAM', 1, GETDATE(), GETDATE()),
(199.99, 'BEATSHEADPHONES', 'Beats Wireless Headphones', 1, GETDATE(), GETDATE()),
(299.99, 'NZXTCASE', 'NZXT H510 Compact ATX Mid-Tower Case', 1, GETDATE(), GETDATE()),
(149.99, 'MICROSOFTOFFICE', 'Microsoft Office 365', 1, GETDATE(), GETDATE()),
(89.99, 'LOGITECHWEBCAM', 'Logitech HD Pro Webcam', 1, GETDATE(), GETDATE()),
(49.99, 'TPLINKROUTER', 'TP-Link AC1750 Smart WiFi Router', 1, GETDATE(), GETDATE()),
(349.99, 'CORSAIRRGBFANS', 'Corsair LL120 RGB Fans (3-Pack)', 1, GETDATE(), GETDATE()),
(899.99, 'ASUSROGSTRIX3080', 'ASUS ROG Strix GeForce RTX 3080', 1, GETDATE(), GETDATE()),
(449.99, 'NVIDIARTX3060TI', 'Nvidia GeForce RTX 3060 Ti', 1, GETDATE(), GETDATE()),
(149.99, 'ASUSPRIMEB550', 'ASUS Prime B550-Plus Motherboard', 1, GETDATE(), GETDATE()),
(249.99, 'NVIDIAGTX1660SUPER', 'Nvidia GeForce GTX 1660 Super', 1, GETDATE(), GETDATE()),
(129.99, 'ASUSVG278Q', 'ASUS VG278Q 27" Full HD Gaming Monitor', 1, GETDATE(), GETDATE()),
(69.99, 'NVIDIAQUADROK4000', 'Nvidia Quadro K4000 Professional Graphics', 1, GETDATE(), GETDATE()),
(299.99, 'ASUSROGTHOR850W', 'ASUS ROG Thor 850W Platinum Power Supply', 1, GETDATE(), GETDATE()),
(119.99, 'NVIDIAGTX1050', 'Nvidia GeForce GTX 1050', 1, GETDATE(), GETDATE()),
(79.99, 'ASUSCERBERUS', 'ASUS Cerberus Gaming Mouse', 1, GETDATE(), GETDATE()),
(399.99, 'NVIDIARTX3070', 'Nvidia GeForce RTX 3070 Founders Edition', 1, GETDATE(), GETDATE()),
(199.99, 'ASUSROGSTRIXB550', 'ASUS ROG Strix B550-F Gaming Motherboard', 1, GETDATE(), GETDATE()),
(149.99, 'NVIDIAQUADRORTX6000', 'Nvidia Quadro RTX 6000', 1, GETDATE(), GETDATE()),
(599.99, 'ASUSROGSTRIXRX6800XT', 'ASUS ROG Strix Radeon RX 6800 XT', 1, GETDATE(), GETDATE()),
(79.99, 'NVIDIAGT1030', 'Nvidia GeForce GT 1030', 1, GETDATE(), GETDATE()),
(149.99, 'ASUSPRIMEZ390A', 'ASUS Prime Z390-A Motherboard', 1, GETDATE(), GETDATE()),
(249.99, 'NVIDIARTX2080SUPER', 'Nvidia GeForce RTX 2080 Super', 1, GETDATE(), GETDATE()),
(129.99, 'ASUSROGSTRIXIMPACT', 'ASUS ROG Strix Impact Gaming Mouse', 1, GETDATE(), GETDATE()),
(399.99, 'NVIDIARTX3080TI', 'Nvidia GeForce RTX 3080 Ti', 1, GETDATE(), GETDATE()),
(199.99, 'ASUSROGSTRIXX570', 'ASUS ROG Strix X570-E Gaming Motherboard', 1, GETDATE(), GETDATE()),
(149.99, 'NVIDIAGTX1660', 'Nvidia GeForce GTX 1660', 1, GETDATE(), GETDATE());

-- Inserts para la tabla ListOption
INSERT INTO [dbo].[ListOption] ([OptionType], [Code], [Name], [Description], [Status], [DateAdded], [DateUpdated])
VALUES
('InvoiceStatus', 'NEW', 'Nuevo', 'Factura Nueva', 1, GETDATE(), GETDATE()),
('InvoiceStatus', 'PENDING', 'Pendiente ', 'Procesamiento Pendiente', 1, GETDATE(), GETDATE()),
('InvoiceStatus', 'ON_PROCESS', 'Nuevo', 'Factura Siendo Procesada', 1, GETDATE(), GETDATE()),
('InvoiceStatus', 'ON_REVISION', 'Nuevo', 'Factura en Revision por departamente Interno', 1, GETDATE(), GETDATE()),
('InvoiceStatus', 'APROVED', 'Nuevo', 'Factura Aprovada Con Exito', 1, GETDATE(), GETDATE()),
('InvoiceStatus', 'SENDED_TO_CLIENT', 'Nuevo', 'Copia de Factura enviada al Cliente', 1, GETDATE(), GETDATE()),
('InvoiceStatus', 'PENDING_PAYMENT', 'Nuevo', 'Pago de Factura Pendiente', 1, GETDATE(), GETDATE()),
('InvoiceStatus', 'PARCIAL_PAYMENT', 'Nuevo', 'Factura Pagada Parcialmente', 1, GETDATE(), GETDATE()),
('InvoiceStatus', 'FULL_PAYMENT', 'Nuevo', 'Factura Pagada', 1, GETDATE(), GETDATE()),
('InvoiceStatus', 'FINISHED', 'Nuevo', 'Factura Finalizada', 1, GETDATE(), GETDATE()),
('InvoiceStatus', 'EXPIRED', 'Nuevo', 'Factura Vencida', 1, GETDATE(), GETDATE()),
('InvoiceStatus', 'REJECTED', 'Nuevo', 'Factura Rechazada (Contante Atencion al cliente)', 1, GETDATE(), GETDATE()),
('InvoiceStatus', 'CANCELED', 'Nuevo', 'Factura Cancelada', 1, GETDATE(), GETDATE()),
('PaymentMethod', 'CASH', 'Efectivo', 'Pago mediante dinero en Efectivo', 1, GETDATE(), GETDATE()),
('PaymentMethod', 'DEBIT', 'Debito', 'Pago mediante tarjeta de Debito', 1, GETDATE(), GETDATE()),
('PaymentMethod', 'CREDIT', 'Credito', 'Pago mediante tarjeta de Credito', 1, GETDATE(), GETDATE()),
('PaymentMethod', 'CUOTES', 'Cuotas', 'Pago en financiamiento de Cuotas', 1, GETDATE(), GETDATE()),
('PaymentMethod', 'TRANSFER', 'Transferencia', 'Pago con transferencia Bancaria', 1, GETDATE(), GETDATE()),
('PaymentMethod', 'VIRTUAL_PAY', 'Billetera Virtual', 'Pago mediante billetera virtual (MP, Uala, Brubank. Etc)', 1, GETDATE(), GETDATE());

