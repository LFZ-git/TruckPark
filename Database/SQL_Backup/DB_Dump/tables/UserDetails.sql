CREATE TABLE [dbo].[UserDetails] (
   [UDID] [int] NOT NULL
      IDENTITY (1,1),
   [OrganizationID] [int] NULL,
   [EmployeeName] [varchar](300) NULL,
   [EmailId] [varchar](100) NULL,
   [MobileNo] [varchar](100) NULL,
   [DepartmentID] [int] NULL,
   [ReportingToID] [int] NULL,
   [Password] [nvarchar](max) NULL,
   [InitialPasswordReset] [bit] NULL,
   [CreatedBy] [int] NULL,
   [CreatedDate] [datetime] NULL,
   [UpdatedBy] [int] NULL,
   [UpdatedDate] [datetime] NULL,
   [IsActive] [bit] NULL,
   [saltKey] [nvarchar](max) NULL,
   [isHOD] [bit] NULL

   ,CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED ([UDID])
)


GO
