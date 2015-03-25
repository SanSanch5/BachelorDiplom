USE [Course_DB]
GO

/****** Object:  Table [dbo].[Routes]    Script Date: 05/12/2014 10:06:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Routes](
	[TransID] [int] NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[ArrTime] [datetime2](7) NULL,
	[CitiesList] [nvarchar](max) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [IX_Routes] UNIQUE NONCLUSTERED 
(
	[TransID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Routes]  WITH CHECK ADD  CONSTRAINT [FK_Routes_Transits] FOREIGN KEY([TransID])
REFERENCES [dbo].[Transits] ([ID])
GO

ALTER TABLE [dbo].[Routes] CHECK CONSTRAINT [FK_Routes_Transits]
GO

