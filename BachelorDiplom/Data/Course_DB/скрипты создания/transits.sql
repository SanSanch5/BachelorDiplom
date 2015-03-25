USE [Course_DB]
GO

/****** Object:  Table [dbo].[Transits]    Script Date: 03/29/2014 11:06:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transits](
	[ID] [int] NOT NULL,
	[DriverID] [int] NOT NULL,
	[ConsID] [int] NOT NULL,
 CONSTRAINT [PK_Transits] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Transits]  WITH CHECK ADD  CONSTRAINT [FK_Transits_Consignments] FOREIGN KEY([ConsID])
REFERENCES [dbo].[Consignments] ([ID])
GO

ALTER TABLE [dbo].[Transits] CHECK CONSTRAINT [FK_Transits_Consignments]
GO

ALTER TABLE [dbo].[Transits]  WITH CHECK ADD  CONSTRAINT [FK_Transits_Drivers] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([ID])
GO

ALTER TABLE [dbo].[Transits] CHECK CONSTRAINT [FK_Transits_Drivers]
GO


