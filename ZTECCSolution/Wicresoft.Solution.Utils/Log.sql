CREATE TABLE [dbo].[Log](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Logger] [nvarchar](500) NULL,
	[Level] [nvarchar](200) NULL,
	[Content] [nvarchar](max) NULL,
	[Stacktrace] [nvarchar](max) NULL,
	[Host] [nvarchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[Url] [nvarchar](2000) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]