CREATE TABLE [dbo].[ExceptionLog] (
   [LogId] [bigint] NOT NULL
      IDENTITY (1,1),
   [ExceptionMsg] [varchar](5000) NULL,
   [ExceptionType] [varchar](500) NULL,
   [ExceptionSource] [nvarchar](max) NULL,
   [ExceptionURL] [varchar](500) NULL,
   [Logdate] [datetime] NULL,
   [OrganizationId] [int] NOT NULL

   ,CONSTRAINT [PK_ExceptionLog] PRIMARY KEY CLUSTERED ([LogId])
)


GO
