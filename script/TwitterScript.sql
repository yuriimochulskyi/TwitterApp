CREATE DATABASE Tweets;
GO

USE [Tweets]
GO

CREATE TABLE Tweet(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StatusID] [numeric](20, 0) NOT NULL,
	[ScreenName] [varchar](50) NULL,
	[Text] [varchar](max) NULL,
	[CreatedAt] [datetime] NULL DEFAULT (getdate()),
	[RTCount] [varchar](20) NULL
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [uidx_statusid] ON [dbo].[Tweet]
(
	[StatusID] ASC
)
