/****** Object:  Table [dbo].[Purchases]    Script Date: 4/27/2019 12:08:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Purchases](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[Name] [varchar](80) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[OrderDate] [varchar](16) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

