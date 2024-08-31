USE [SPECIMEN_TRACKING]
GO

/****** Object:  Table [dbo].[FUNCTIONS]    Script Date: 31/08/2024 3:49:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FUNCTIONS](
	[FunctionID] [int] IDENTITY(200,1) NOT NULL,
	[FunctionName] [nvarchar](255) NULL,
	[Parent] [nvarchar](50) NULL,
	[LevelID] [int] NULL,
	[SequenceID] [int] NULL,
	[NavigationURL] [nvarchar](255) NULL,
	[Active] [bit] NULL,
	[AutoActive] [bit] NULL,
	[Description] [ntext] NULL,
	[DtEntered] [datetime] NULL,
	[EnteredBy] [nvarchar](50) NULL,
	[Filter] [nvarchar](max) NULL,
	[PROJECTID] [int] NULL,
	[SystemID] [int] NULL,
	[SystemName] [nvarchar](50) NULL,
	[Color] [nvarchar](50) NULL,
	[Icon] [nvarchar](50) NULL,
 CONSTRAINT [PK_Functions_1] PRIMARY KEY CLUSTERED 
(
	[FunctionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[FUNCTIONS] ADD  CONSTRAINT [DF_Functions_Active]  DEFAULT ((0)) FOR [Active]
GO


