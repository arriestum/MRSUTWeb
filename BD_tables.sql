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

CREATE TABLE [dbo].[Userr](
[ID_User]		[int] PRIMARY KEY,
[Username]	[nvarchar](50) NOT NULL,
[Password]	[nvarchar](50) NOT NULL,
[Email]		[nvarchar](50) NOT NULL,
[LastLogin]	[datetime] NOT NULL,
[LastIp]	[nvarchar](30) NOT NULL,

)

GO 
CREATE TABLE [dbo].[Tip_tranzactie](
[ID_tip_tranzactie] [int] PRIMARY KEY,
[Name_transaction] 	[nvarchar](50) NOT NULL,
);

GO

CREATE TABLE [dbo].[Status_card](
[ID_status]       [int] PRIMARY KEY,
[Status] 		  [nvarchar](50) NOT NULL,
);

GO
CREATE TABLE [dbo].[Tip_card](
[Type_ID_card] [int] PRIMARY KEY,
[Name] 		   [nvarchar](50) NOT NULL,
);

GO


CREATE TABLE [dbo].[Cardd](
[ID_Card]		[int] PRIMARY KEY,
[Type_ID_Card]	[int] FOREIGN KEY REFERENCES Tip_card(Type_ID_Card),
[ID_Status]		[int] FOREIGN KEY REFERENCES Status_card(ID_Status),
[ID_User]		[int] FOREIGN KEY REFERENCES Userr(ID_User),
[Number]		[char](16) NOT NULL,
[CVV]			[char](3) NOT NULL,
[Balance]		[float](50) NOT NULL,
);

GO 
CREATE TABLE [dbo].[Transaction](
[ID_Transaction]	      [int] PRIMARY KEY,
[ID_card_sourse]	      [int] FOREIGN KEY REFERENCES Cardd(ID_Card),
[ID_tip_tranzactie]		  [int] FOREIGN KEY REFERENCES Tip_tranzactie(ID_tip_tranzactie),
[Number]				  [char](16) NOT NULL,
[Date]			          [datetime] NOT NULL,
[Balance] 	              [float](50) NOT NULL,
);

GO





