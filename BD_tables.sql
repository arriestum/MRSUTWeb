USE MASTER
GO
IF EXISTS(SELECT * FROM sys.databases WHERE NAME='MRSUTWeb')
BEGIN
ALTER DATABASE MRSUTWeb SET SINGLE_USER
WITH ROLLBACK IMMEDIATE
DROP DATABASE MRSUTWeb
END
GO
CREATE DATABASE MRSUTWeb
GO
USE MRSUTWeb
GO

CREATE TABLE [dbo].[User](
[Id]		[int] IDENTITY(1,1) NOT NULL,
[Username]	[nvarchar](50) NOT NULL,
[Password]	[nvarchar](50) NOT NULL,
[Email]		[nvarchar](50) NOT NULL,
[LastLogin]	[datetime] NOT NULL,
[LastIp]	[nvarchar](30) NOT NULL,
CONSTRAINT	[PK_UDbTables] PRIMARY KEY CLUSTERED 
([Id] ASC)
);

GO 

CREATE TABLE [dbo].[Card](
[ID_Card]		[int] IDENTITY(1,1) NOT NULL,
[Type_ID_Card]	[int] IDENTITY(1,1) NOT NULL,
[ID_Status]		[int] IDENTITY(1,1) NOT NULL,
[ID_User]		[int] IDENTITY(1,1) NOT NULL,
[Number]		[char](16) NOT NULL,
[CVV]			[char](3) NOT NULL,
[Balance]		[float](50) NOT NULL,
);

GO 

CREATE TABLE [dbo].[Transaction](
[ID_Transaction]	      [int] IDENTITY(1,1) NOT NULL,
[ID_card_sourse]	      [int] IDENTITY(1,1) NOT NULL,
[Number_card_destination] [int] IDENTITY(1,1) NOT NULL,
[Date]			          [datetime] NOT NULL,
[Balance] 	              [float](50) NOT NULL,
);

GO

CREATE TABLE [dbo].[Tip_card](
[Type_ID_card] [int] IDENTITY(1,1) NOT NULL,
[Name] 		   [nvarchar](50) NOT NULL,
);

GO 

CREATE TABLE [dbo].[Status_card](
[ID_status]       [int] IDENTITY(1,1) NOT NULL,
[Status] 		  [nvarchar](50) NOT NULL,
);

GO

CREATE TABLE [dbo].[Tip_tranzactie](
[ID_tip_tranzactie] [int] IDENTITY(1,1) NOT NULL,
[Name_transaction] 	[nvarchar](50) NOT NULL,
);

GO
