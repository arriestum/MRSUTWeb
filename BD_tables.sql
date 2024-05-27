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


CREATE TABLE [dbo].[Type_user](
[ID_Type_user] [int] PRIMARY KEY,
[Name] 		   [nvarchar](50) NOT NULL
)
GO
INSERT INTO Type_user VALUES
(1,'Neconfirmat'),
(2,'Confirmat')
GO

CREATE TABLE [dbo].[Userr](
[ID_User]		[int] PRIMARY KEY IDENTITY,
[ID_Type_user] [int] FOREIGN KEY REFERENCES Type_user(ID_Type_user),
[Name]	[nvarchar](50) NOT NULL,
[Surname]	[nvarchar](50) NOT NULL,
[Username]	[nvarchar](50) NOT NULL,
[Hash_Password]	[nvarchar](256) NOT NULL,
[Salt] [nvarchar](50) NOT NULL,
[Email]		[nvarchar](50) NOT NULL,
[Code] [varchar](6) NOT NULL
)
GO 
CREATE TABLE [dbo].[Tip_tranzactie](
[ID_tip_tranzactie] [int] PRIMARY KEY IDENTITY,
[Name_transaction] 	[nvarchar](50) NOT NULL
);

GO

CREATE TABLE [dbo].[Status_card](
[ID_status]       [int] PRIMARY KEY IDENTITY,
[Status] 		  [nvarchar](50) NOT NULL,
);

GO
CREATE TABLE [dbo].[Tip_card](
[ID_Type_card] [int] PRIMARY KEY IDENTITY,
[Name] 		   [nvarchar](50) NOT NULL
);

GO


CREATE TABLE [dbo].[Cardd](
[ID_Card]		[int] PRIMARY KEY IDENTITY,
[ID_Type_card]	[int] FOREIGN KEY REFERENCES Tip_card(ID_Type_card),
[ID_Status]		[int] FOREIGN KEY REFERENCES Status_card(ID_Status),
[ID_User]		[int] FOREIGN KEY REFERENCES Userr(ID_User),
[Number]		[char](16) NOT NULL,
[CVV]			[char](3) NOT NULL,
[Balance]		[float](50) NOT NULL
);

GO 
CREATE TABLE [dbo].[Transaction](
[ID_Transaction]	      [int] PRIMARY KEY IDENTITY,
[ID_card_sourse]	      [int] FOREIGN KEY REFERENCES Cardd(ID_Card),
[ID_tip_tranzactie]		  [int] FOREIGN KEY REFERENCES Tip_tranzactie(ID_tip_tranzactie),
[Number]				  [char](16) NOT NULL,
[Date]			          [datetime] NOT NULL,
[Balance] 	              [float](50) NOT NULL
);
GO
CREATE TABLE [dbo].[Sessions](
ID_Session [int] PRIMARY KEY IDENTITY,
ID_User [int] FOREIGN KEY REFERENCES Userr(ID_User),
SessionToken [nvarchar](100) NOT NULL,
ExpiryDateTime [datetime] NOT NULL
);
GO