CREATE TABLE [dbo].[Organization] (
   [OrganizationID] [smallint] NOT NULL
      IDENTITY (1,1),
   [CompanyName] [varchar](2000) NULL,
   [CompanyShortName] [varchar](500) NULL,
   [OrganizationCreatedDate] [datetime] NULL,
   [IsActive] [bit] NULL,
   [OrganizationTypeId] [smallint] NOT NULL,
   [CompanyAddress] [varchar](500) NULL

   ,CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED ([OrganizationID])
)


GO
