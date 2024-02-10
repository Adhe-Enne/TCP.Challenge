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
	[CUIT] [VARCHAR](50),
	[Adress] [VARCHAR](255),
	[Phone] [VARCHAR](50),
	[Email] [VARCHAR](50),
	[Disabled] [BIT],
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
	[Code] [VARCHAR](50),
	[Name] [VARCHAR](50),
	[Description] [VARCHAR](255),
	[Status] [INT] NOT NULL,
	[DateAdded] [DATETIME],
	[DateUpdated] [DATETIME]
CONSTRAINT PK_ListOption PRIMARY KEY ([Id])
)