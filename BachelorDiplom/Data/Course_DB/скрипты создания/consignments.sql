USE [Course_DB]
GO

/****** Object:  Table [dbo].[Consignments]    Script Date: 03/29/2014 11:05:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Consignments](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Danger_degree] [int] NOT NULL,
	[After_crash] [nvarchar](max) NULL,
 CONSTRAINT [PK_Consignments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


