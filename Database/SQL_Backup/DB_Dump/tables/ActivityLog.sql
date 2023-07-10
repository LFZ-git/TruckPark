CREATE TABLE [dbo].[ActivityLog] (
   [ActivityId] [bigint] NOT NULL
      IDENTITY (1,1),
   [ActivityType] [varchar](500) NOT NULL,
   [UserId] [int] NOT NULL,
   [Activitydate] [datetime] NULL,
   [ModuleId] [int] NULL,
   [RoleId] [int] NULL,
   [Remarks] [varchar](500) NULL,
   [OrganizationId] [int] NOT NULL

   ,CONSTRAINT [PK_ActivityLog] PRIMARY KEY CLUSTERED ([ActivityId])
)


GO
