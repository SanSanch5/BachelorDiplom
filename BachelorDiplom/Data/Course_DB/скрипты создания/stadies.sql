USE [Course_DB]
GO

/****** Object:  Table [dbo].[TransitStadies]    Script Date: 03/29/2014 11:07:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TransitStadies](
	[TransID] [int] NOT NULL,
	[CityID] [int] NOT NULL,
	[LocationTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TransitStadies] PRIMARY KEY CLUSTERED 
(
	[TransID] ASC,
	[CityID] ASC,
	[LocationTime] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TransitStadies]  WITH CHECK ADD  CONSTRAINT [FK_TransitStadies_Cities] FOREIGN KEY([CityID])
REFERENCES [dbo].[Cities] ([ID])
GO

ALTER TABLE [dbo].[TransitStadies] CHECK CONSTRAINT [FK_TransitStadies_Cities]
GO

ALTER TABLE [dbo].[TransitStadies]  WITH CHECK ADD  CONSTRAINT [FK_TransitStadies_Transits] FOREIGN KEY([TransID])
REFERENCES [dbo].[Transits] ([ID])
GO

ALTER TABLE [dbo].[TransitStadies] CHECK CONSTRAINT [FK_TransitStadies_Transits]
GO


