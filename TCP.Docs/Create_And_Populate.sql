USE [Master]
GO

IF DB_ID('TCP.FacturadorDB') IS NOT NULL
	BEGIN 
	DROP DATABASE [TCP.FacturadorDB]
	PRINT 'La base de datos fue reseteada'
	END
GO
CREATE DATABASE [TCP.FacturadorDB] 
GO
USE [TCP.FacturadorDB]
GO

CREATE TABLE [dbo].[Client](
	[Id] [INT] IDENTITY(1000,1) NOT NULL,
	[CompanyName] [VARCHAR](255),
	[CUIT] [VARCHAR](50),
	[Adress] [VARCHAR](255),
	[Disabled] [BIT]
	[DateAdded] [DATETIME],
	[DateUpdated] [DATETIME]

CONSTRAINT PK_Client PRIMARY KEY ([Id])
)
GO


CREATE TABLE [dbo].[Invoice](
	[Id] [INT] IDENTITY(2000,1) NOT NULL,
	[ClientId] [INT] NOT NULL,
	[DateAdded] [DATETIME],
	[DateUpdated] [DATETIME]
	[Status] [VARCHAR](50)
	
CONSTRAINT PK_Invoice PRIMARY KEY ([Id]),
CONSTRAINT FK_Invoice_Client FOREIGN KEY (ClientId) REFERENCES Client (Id)
)

GO


CREATE TABLE [dbo].[Invoice_Detail](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[FactID] [INT] NOT NULL,
	[DateAdded] [DATETIME],
	[DateUpdated] [DATETIME]
	[ProductId] [VARCHAR](50),
	[Qty] [DECIMAL],
	[Price] [DECIMAL],
	[LineAmount] [DECIMAL](30),
		
CONSTRAINT PK_Invoice_Detail PRIMARY KEY ([Id]),
CONSTRAINT FK_Invoice FOREIGN KEY(FactID) REFERENCES Invoice (Id)
)	
GO

CREATE TABLE [dbo].[Product](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[DateAdded] [DATETIME],
	[DateUpdated] [DATETIME]
	[Status] [INT] NOT NULL,
	[Price] [DECIMAL],
	[Code] [VARCHAR](50),
	[Description] [VARCHAR](255)
		
CONSTRAINT PK_Product PRIMARY KEY ([Id])
)	
GO

CREATE TABLE [dbo].[ListOption](
	[Id] [INT] IDENTITY(1,1) NOT NULL,
	[OptionType] [VARCHAR](50),
	[Code] [VARCHAR](50),
	[Name] [VARCHAR](50),
	[Description] [VARCHAR](255),
	[DateAdded] [DATETIME],
	[DateUpdated] [DATETIME]
	
CONSTRAINT PK_Invoice PRIMARY KEY ([Id]),
CONSTRAINT FK_Invoice_Client FOREIGN KEY (Cli_Id) REFERENCES Client (Id)
)